using GymSimulator.Application.DTOs;
using GymSimulator.Domain.Entities;

namespace GymSimulator.Application.Abstractions;

public interface IClassService
{
    Task<IEnumerable<Class>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Class?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Class> CreateAsync(ClassCreateDto dto, CancellationToken cancellationToken = default);
    Task<Class?> UpdateAsync(Guid id, ClassUpdateDto dto, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}


