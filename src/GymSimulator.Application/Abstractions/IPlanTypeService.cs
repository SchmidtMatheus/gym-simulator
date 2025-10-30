using GymSimulator.Application.DTOs;
using GymSimulator.Domain.Entities;

namespace GymSimulator.Application.Abstractions;

public interface IPlanTypeService
{
    Task<IEnumerable<PlanType>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<PlanType?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<PlanType> CreateAsync(PlanTypeCreateDto dto, CancellationToken cancellationToken = default);
    Task<PlanType?> UpdateAsync(Guid id, PlanTypeUpdateDto dto, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}


