using GymSimulator.Application.DTOs;
using GymSimulator.Domain.Entities;

namespace GymSimulator.Application.Abstractions;

public interface IClassTypeService
{
    Task<IEnumerable<ClassType>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<ClassType?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<ClassType> CreateAsync(ClassTypeCreateDto dto, CancellationToken cancellationToken = default);
    Task<ClassType?> UpdateAsync(Guid id, ClassTypeUpdateDto dto, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}


