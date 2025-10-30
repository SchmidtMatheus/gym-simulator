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


