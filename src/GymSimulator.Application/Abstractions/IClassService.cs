using GymSimulator.Application.DTOs;
using GymSimulator.Domain.Entities;

namespace GymSimulator.Application.Abstractions;

public interface IClassService
{
    Task<IEnumerable<Class>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Class?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<Class> CreateAsync(ClassCreateDto dto, CancellationToken cancellationToken = default);
    Task<Class?> UpdateAsync(int id, ClassUpdateDto dto, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
}


