using GymSimulator.Application.DTOs;
using GymSimulator.Domain.Entities;

namespace GymSimulator.Application.Abstractions;

public interface IPlanTypeService
{
    Task<IEnumerable<PlanType>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<PlanType?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<PlanType> CreateAsync(PlanTypeCreateDto dto, CancellationToken cancellationToken = default);
    Task<PlanType?> UpdateAsync(int id, PlanTypeUpdateDto dto, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
}


