using GymSimulator.Application.Abstractions;
using GymSimulator.Application.DTOs;
using GymSimulator.Domain.Entities;
using GymSimulator.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GymSimulator.Infrastructure.Services;

public class PlanTypeService : IPlanTypeService
{
    private readonly AppDbContext _dbContext;

    public PlanTypeService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<PlanType>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.PlanTypes.AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<PlanType?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.PlanTypes.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<PlanType> CreateAsync(PlanTypeCreateDto dto, CancellationToken cancellationToken = default)
    {
        var entity = new PlanType
        {
            Name = dto.Name,
            Description = dto.Description,
            MonthlyClassLimit = dto.MonthlyClassLimit,
            IsActive = dto.IsActive,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
        _dbContext.PlanTypes.Add(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<PlanType?> UpdateAsync(Guid id, PlanTypeUpdateDto dto, CancellationToken cancellationToken = default)
    {
        var entity = await _dbContext.PlanTypes.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        if (entity is null) return null;
        entity.Name = dto.Name;
        entity.Description = dto.Description;
        entity.MonthlyClassLimit = dto.MonthlyClassLimit;
        entity.IsActive = dto.IsActive;
        entity.UpdatedAt = DateTime.UtcNow;
        await _dbContext.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await _dbContext.PlanTypes.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        if (entity is null) return false;
        _dbContext.PlanTypes.Remove(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }
}


