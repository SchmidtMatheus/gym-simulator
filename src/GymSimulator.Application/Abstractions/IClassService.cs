using GymSimulator.Application.DTOs;
using GymSimulator.Application.DTOs.Common;
using GymSimulator.Domain.Entities;

namespace GymSimulator.Application.Abstractions;

public interface IClassService
{
    Task<PagedResponse<ClassDto>> GetAllAsync(PagedRequest request, CancellationToken cancellationToken = default);
    Task<ClassDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<ClassDto> CreateAsync(ClassCreateDto dto, CancellationToken cancellationToken = default);
    Task<ClassDto?> UpdateAsync(Guid id, ClassUpdateDto dto, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}


