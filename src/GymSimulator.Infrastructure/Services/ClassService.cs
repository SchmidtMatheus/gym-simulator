using GymSimulator.Application.Abstractions;
using GymSimulator.Application.DTOs;
using GymSimulator.Application.DTOs.Common;
using GymSimulator.Application.Mappings;
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

    public async Task<PagedResponse<ClassDto>> GetAllAsync(PagedRequest request, CancellationToken cancellationToken = default)
    {
        var query = _dbContext.Classes
                .Include(c => c.ClassType)
                .AsQueryable();

        var totalCount = await query.CountAsync();

        var classes = await query
                .OrderByDescending(c => c.ScheduledAt)
                .Skip(request.Skip)
                .Take(request.Take)
                .ToListAsync(cancellationToken);

        var classDtos = classes.ToDto();

        return new PagedResponse<ClassDto>(
            classDtos,
            totalCount,
            request.PageNumber,
            request.PageSize
        );
    }

    public async Task<ClassDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
       var query = await _dbContext.Classes
            .Include(c => c.ClassType)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if(query == null) 
            return null;

        return query.ToDto();
    }
    public async Task<PagedResponse<ClassDto>> GetAvailableAsync(PagedRequest request, CancellationToken cancellationToken = default)
    {
        var query = _dbContext.Classes
            .Include(c => c.ClassType)
            .Where(c => c.IsActive && !c.IsCancelled && c.CurrentParticipants < c.MaxCapacity)
            .AsQueryable();

        var totalCount = await query.CountAsync();

        var availableClasses = await query
            .OrderBy(c => c.ScheduledAt)
            .Skip(request.Skip)
            .Take(request.Take)
            .ToListAsync(cancellationToken);

        var availableClassDtos = availableClasses.ToDto();

        return new PagedResponse<ClassDto>(
            availableClassDtos,
            totalCount,
            request.PageNumber,
            request.PageSize
        );
    }

    public async Task<ClassDto> CreateAsync(ClassCreateDto dto, CancellationToken cancellationToken = default)
    {
        var startTime = dto.ScheduledAt;
        var endTime = dto.ScheduledAt.AddMinutes(dto.DurationMinutes);

        var hasConflict = await _dbContext.Classes
            .AnyAsync(c =>
                c.ClassTypeId == dto.ClassTypeId &&
                !c.IsCancelled &&
                (
                    (startTime >= c.ScheduledAt && startTime < c.ScheduledAt.AddMinutes(c.DurationMinutes)) ||
                    (endTime > c.ScheduledAt && endTime <= c.ScheduledAt.AddMinutes(c.DurationMinutes)) ||
                    (startTime <= c.ScheduledAt && endTime >= c.ScheduledAt.AddMinutes(c.DurationMinutes))
                ),
                cancellationToken
            );

        if (hasConflict)
        {
            throw new InvalidOperationException("Já existe uma aula do mesmo tipo neste período.");
        }

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
        return entity.ToDto();
    }

    public async Task<ClassDto?> UpdateAsync(Guid id, ClassUpdateDto dto, CancellationToken cancellationToken = default)
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
        return entity.ToDto();
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await _dbContext.Classes.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        if (entity is null) return false;
        _dbContext.Classes.Remove(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }
}


