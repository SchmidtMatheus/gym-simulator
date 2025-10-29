using GymSimulator.Application.Abstractions;
using GymSimulator.Application.DTOs;
using GymSimulator.Domain.Entities;
using GymSimulator.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GymSimulator.Infrastructure.Services;

public class ClassService : IClassService
{
    private readonly AppDbContext _dbContext;

    public ClassService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Class>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Classes
            .AsNoTracking()
            .Include(c => c.ClassType)
            .ToListAsync(cancellationToken);
    }

    public async Task<Class?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Classes
            .Include(c => c.ClassType)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<Class> CreateAsync(ClassCreateDto dto, CancellationToken cancellationToken = default)
    {
        var entity = new Class
        {
            ClassTypeId = dto.ClassTypeId,
            ScheduledAt = dto.ScheduledAt,
            DurationMinutes = dto.DurationMinutes,
            MaxCapacity = dto.MaxCapacity,
            IsActive = dto.IsActive,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
        _dbContext.Classes.Add(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<Class?> UpdateAsync(int id, ClassUpdateDto dto, CancellationToken cancellationToken = default)
    {
        var entity = await _dbContext.Classes.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        if (entity is null) return null;
        entity.ClassTypeId = dto.ClassTypeId;
        entity.ScheduledAt = dto.ScheduledAt;
        entity.DurationMinutes = dto.DurationMinutes;
        entity.MaxCapacity = dto.MaxCapacity;
        entity.IsActive = dto.IsActive;
        entity.IsCancelled = dto.IsCancelled;
        entity.UpdatedAt = DateTime.UtcNow;
        await _dbContext.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await _dbContext.Classes.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        if (entity is null) return false;
        _dbContext.Classes.Remove(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }
}


