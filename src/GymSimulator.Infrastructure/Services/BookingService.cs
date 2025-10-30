using GymSimulator.Application.Abstractions;
using GymSimulator.Application.DTOs;
using GymSimulator.Application.DTOs.Common;
using GymSimulator.Application.Mappings;
using GymSimulator.Domain.Entities;
using GymSimulator.Domain.Enums;
using GymSimulator.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GymSimulator.Infrastructure.Services;

public class BookingService : IBookingService
{
    private readonly AppDbContext _dbContext;

    public BookingService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<PagedResponse<BookingDto>> GetAllAsync(PagedRequest request, CancellationToken cancellationToken = default)
    {
        var query = _dbContext.Bookings
            .Include(b => b.Student)
            .Include(b => b.Class)
            .AsQueryable();

        var totalCount = await query.CountAsync();

        var bookings = await query
                .OrderByDescending(s => s.CreatedAt)
                .Skip(request.Skip)
                .Take(request.Take)
                .ToListAsync(cancellationToken);

        var bookingDtos = bookings.ToDto();

        return new PagedResponse<BookingDto>(
            bookingDtos, totalCount, request.PageNumber, request.PageSize);
    }

    public async Task<BookingDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var query = await _dbContext.Bookings
            .Include(b => b.Student)
            .Include(b => b.Class)
            .FirstOrDefaultAsync(b => b.Id == id, cancellationToken);

        if (query == null)
            return null;

        var bookingDto = query.ToDto();
        return bookingDto;
    }

    public async Task<BookingDto> CreateAsync(BookingCreateDto dto, CancellationToken cancellationToken = default)
    {
        var student = await _dbContext.Students.Include(s => s.PlanType).FirstOrDefaultAsync(s => s.Id == dto.StudentId, cancellationToken)
            ?? throw new InvalidOperationException("Aluno não encontrado");
        var gymClass = await _dbContext.Classes.FirstOrDefaultAsync(c => c.Id == dto.ClassId, cancellationToken)
            ?? throw new InvalidOperationException("Aula não encontrada");

        if (!student.IsActive) throw new InvalidOperationException("Aluno não está ativo no sistema");
        if (!gymClass.IsActive || gymClass.IsCancelled) throw new InvalidOperationException("Aula não disponível");

        if (gymClass.CurrentParticipants >= gymClass.MaxCapacity)
            throw new InvalidOperationException("Aula já está cheia!");

        var currentMonth = DateTime.UtcNow.ToString("yyyyMM");
        if (student.CurrentMonthYear != currentMonth)
        {
            student.CurrentMonthYear = currentMonth;
            student.MonthlyClassCount = 0;
        }
        if (student.MonthlyClassCount >= student.PlanType.MonthlyClassLimit)
            throw new InvalidOperationException("Número máximo de aulas para o seu plano já foi atingido!");

        var booking = new Booking
        {
            StudentId = dto.StudentId,
            ClassId = dto.ClassId,
            BookingStatus = BookingStatus.Agendado,
            BookingDate = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        gymClass.CurrentParticipants += 1;
        student.MonthlyClassCount += 1;
        _dbContext.Bookings.Add(booking);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return booking.ToDto();
    }

    public async Task<bool> CancelAsync(Guid id, string? reason = null, CancellationToken cancellationToken = default)
    {
        var booking = await _dbContext.Bookings.Include(b => b.Class).Include(b => b.Student).FirstOrDefaultAsync(b => b.Id == id, cancellationToken);
        if (booking is null) return false;

        if (booking.BookingStatus == BookingStatus.Cancelado)
            return true;

        booking.BookingStatus = BookingStatus.Cancelado;
        booking.CancelledAt = DateTime.UtcNow;
        booking.UpdatedAt = DateTime.UtcNow;

        if (booking.Class.CurrentParticipants > 0)
            booking.Class.CurrentParticipants -= 1;
        if (booking.Student.MonthlyClassCount > 0)
            booking.Student.MonthlyClassCount -= 1;

        await _dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }
}


