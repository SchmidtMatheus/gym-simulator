using GymSimulator.Application.DTOs;
using GymSimulator.Domain.Entities;

namespace GymSimulator.Application.Abstractions;

public interface IClassTypeService
{
    Task<IEnumerable<ClassType>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<ClassType?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<ClassType> CreateAsync(ClassTypeCreateDto dto, CancellationToken cancellationToken = default);
    Task<ClassType?> UpdateAsync(int id, ClassTypeUpdateDto dto, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
}


