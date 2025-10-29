using GymSimulator.Application.Abstractions;
using GymSimulator.Application.DTOs;
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

    public async Task<IEnumerable<Student>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Students
            .AsNoTracking()
            .Include(s => s.PlanType)
            .ToListAsync(cancellationToken);
    }

    public async Task<Student?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Students
            .Include(s => s.PlanType)
            .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
    }

    public async Task<Student> CreateAsync(StudentCreateDto dto, CancellationToken cancellationToken = default)
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
        return entity;
    }

    public async Task<Student?> UpdateAsync(int id, StudentUpdateDto dto, CancellationToken cancellationToken = default)
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
        return entity;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await _dbContext.Students.FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
        if (entity is null) return false;

        _dbContext.Students.Remove(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }
}


