using GymSimulator.Application.Abstractions;
using GymSimulator.Application.DTOs;
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

    public async Task<IEnumerable<Booking>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Bookings
            .AsNoTracking()
            .Include(b => b.Student)
            .Include(b => b.Class)
            .ToListAsync(cancellationToken);
    }

    public async Task<Booking?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Bookings
            .Include(b => b.Student)
            .Include(b => b.Class)
            .FirstOrDefaultAsync(b => b.Id == id, cancellationToken);
    }

    public async Task<Booking> CreateAsync(BookingCreateDto dto, CancellationToken cancellationToken = default)
    {
        var student = await _dbContext.Students.Include(s => s.PlanType).FirstOrDefaultAsync(s => s.Id == dto.StudentId, cancellationToken)
            ?? throw new InvalidOperationException("Student not found");
        var gymClass = await _dbContext.Classes.FirstOrDefaultAsync(c => c.Id == dto.ClassId, cancellationToken)
            ?? throw new InvalidOperationException("Class not found");

        if (!student.IsActive) throw new InvalidOperationException("Student is not active");
        if (!gymClass.IsActive || gymClass.IsCancelled) throw new InvalidOperationException("Class is not available");

        // enforce capacity
        if (gymClass.CurrentParticipants >= gymClass.MaxCapacity)
            throw new InvalidOperationException("Class is full");

        // enforce monthly plan limit
        var currentMonth = DateTime.UtcNow.ToString("yyyyMM");
        if (student.CurrentMonthYear != currentMonth)
        {
            student.CurrentMonthYear = currentMonth;
            student.MonthlyClassCount = 0;
        }
        if (student.MonthlyClassCount >= student.PlanType.MonthlyClassLimit)
            throw new InvalidOperationException("Monthly class limit reached");

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
        return booking;
    }

    public async Task<bool> CancelAsync(int id, string? reason = null, CancellationToken cancellationToken = default)
    {
        var booking = await _dbContext.Bookings.Include(b => b.Class).Include(b => b.Student).FirstOrDefaultAsync(b => b.Id == id, cancellationToken);
        if (booking is null) return false;

        if (booking.BookingStatus == BookingStatus.Cancelado)
            return true;

        booking.BookingStatus = BookingStatus.Cancelado;
        booking.CancelledAt = DateTime.UtcNow;
        booking.UpdatedAt = DateTime.UtcNow;

        // free spot and revert monthly count (simple approach)
        if (booking.Class.CurrentParticipants > 0)
            booking.Class.CurrentParticipants -= 1;
        if (booking.Student.MonthlyClassCount > 0)
            booking.Student.MonthlyClassCount -= 1;

        await _dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }
}


