using GymSimulator.Application.Abstractions;
using GymSimulator.Application.DTOs;
using GymSimulator.Domain.Entities;
using GymSimulator.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GymSimulator.Infrastructure.Services;

public class ClassTypeService : IClassTypeService
{
    private readonly AppDbContext _dbContext;

    public ClassTypeService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<ClassType>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.ClassTypes.AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<ClassType?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.ClassTypes.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<ClassType> CreateAsync(ClassTypeCreateDto dto, CancellationToken cancellationToken = default)
    {
        var entity = new ClassType
        {
            Name = dto.Name,
            Description = dto.Description,
            IntensityLevel = dto.IntensityLevel,
            IsActive = dto.IsActive,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
        _dbContext.ClassTypes.Add(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<ClassType?> UpdateAsync(int id, ClassTypeUpdateDto dto, CancellationToken cancellationToken = default)
    {
        var entity = await _dbContext.ClassTypes.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        if (entity is null) return null;
        entity.Name = dto.Name;
        entity.Description = dto.Description;
        entity.IntensityLevel = dto.IntensityLevel;
        entity.IsActive = dto.IsActive;
        entity.UpdatedAt = DateTime.UtcNow;
        await _dbContext.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await _dbContext.ClassTypes.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        if (entity is null) return false;
        _dbContext.ClassTypes.Remove(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }
}


