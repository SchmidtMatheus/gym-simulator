using GymSimulator.Application.Abstractions;
using GymSimulator.Application.DTOs;
using GymSimulator.Application.DTOs.Common;
using GymSimulator.Application.Mappings;
using GymSimulator.Domain.Entities;
using GymSimulator.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GymSimulator.Infrastructure.Services;

public class StudentService : IStudentService
{
    private readonly AppDbContext _dbContext;

    public StudentService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<PagedResponse<StudentDto>> GetAllAsync(PagedRequest request, CancellationToken cancellationToken = default)
    {
        var query = _dbContext.Students
                .Include(s => s.PlanType)
                .AsQueryable();
        var totalCount = await query.CountAsync();

        var students = await query
                .OrderBy(s => s.Name)
                .Skip(request.Skip)
                .Take(request.Take)
                .ToListAsync(cancellationToken);

        var studentDtos = students.ToDto();
        return new PagedResponse<StudentDto>(
            studentDtos, totalCount, request.PageNumber, request.PageSize);
    }

    public async Task<StudentDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var student = await _dbContext.Students
                .Include(s => s.PlanType)
                .FirstOrDefaultAsync(s => s.Id == id);

        return student?.ToDto();
    }

    public async Task<StudentReportDto> GetStudentReportAsync(Guid studentId, CancellationToken cancellationToken)
    {
        var today = DateTime.UtcNow;
        return await GetStudentReportAsync(studentId, today.Month, today.Year, cancellationToken);
    }

    public async Task<StudentReportDto> GetStudentReportAsync(Guid studentId, int month, int year, CancellationToken cancellationToken)
    {
        var student = await _dbContext.Students
            .FirstOrDefaultAsync(s => s.Id == studentId);

        if (student == null)
        {
            throw new ArgumentException($"Aluno com ID {studentId} não encontrado.");
        }

        var startDate = new DateTime(year, month, 1);
        var endDate = startDate.AddMonths(1).AddDays(-1);
        var tz = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");
        var startDateUtc = TimeZoneInfo.ConvertTimeToUtc(startDate, tz);
        var endDateUtc = TimeZoneInfo.ConvertTimeToUtc(endDate.AddDays(1).AddTicks(-1), tz);

        var studentBookings = await _dbContext.Bookings
            .Include(b => b.Class)
                .ThenInclude(c => c.ClassType)
            .Where(b => b.StudentId == studentId &&
                       b.Class.ScheduledAt >= startDateUtc &&
                       b.Class.ScheduledAt <= endDateUtc &&
                       b.BookingStatus != Domain.Enums.BookingStatus.Cancelado)
            .ToListAsync(cancellationToken);

        var totalClasses = studentBookings.Count;

        var classTypeFrequency = studentBookings
            .GroupBy(b => new { b.Class.ClassTypeId, b.Class.ClassType.Name })
            .Select(g => new ClassTypeFrequencyDto
            {
                ClassTypeId = g.Key.ClassTypeId,
                ClassTypeName = g.Key.Name,
                BookingCount = g.Count(),
                Percentage = totalClasses > 0 ? (double)g.Count() / totalClasses * 100 : 0
            })
            .OrderByDescending(x => x.BookingCount)
            .Take(5)
            .ToList();

        return new StudentReportDto
        {
            StudentId = student.Id,
            StudentName = student.Name,
            StudentEmail = student.Email,
            TotalClassesThisMonth = totalClasses,
            MostFrequentClassTypes = classTypeFrequency,
            ReportDate = DateTime.UtcNow
        };
    }


    public async Task<StudentDto> CreateAsync(StudentCreateDto dto, CancellationToken cancellationToken = default)
    {
        var entity = new Student
        {
            Name = dto.Name,
            Email = dto.Email,
            Phone = dto.Phone,
            PlanTypeId = dto.PlanTypeId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            IsActive = true,
            MonthlyClassCount = 0,
            CurrentMonthYear = DateTime.UtcNow.ToString("yyyyMM")
        };

        _dbContext.Students.Add(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return entity.ToDto();
    }

    public async Task<StudentDto?> UpdateAsync(Guid id, StudentUpdateDto dto, CancellationToken cancellationToken = default)
    {
        var entity = await _dbContext.Students.FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
        if (entity is null) return null;

        entity.Name = dto.Name;
        entity.Email = dto.Email;
        entity.Phone = dto.Phone;
        entity.PlanTypeId = dto.PlanTypeId;
        entity.IsActive = dto.IsActive;
        entity.UpdatedAt = DateTime.UtcNow;

        await _dbContext.SaveChangesAsync(cancellationToken);
        return entity.ToDto();
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await _dbContext.Students.FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
        if (entity is null) return false;

        _dbContext.Students.Remove(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }
}


