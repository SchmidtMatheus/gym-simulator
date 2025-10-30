using GymSimulator.Application.DTOs;
using GymSimulator.Domain.Entities;

namespace GymSimulator.Application.Abstractions;

public interface IStudentService
{
    Task<IEnumerable<Student>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Student?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Student> CreateAsync(StudentCreateDto dto, CancellationToken cancellationToken = default);
    Task<Student?> UpdateAsync(Guid id, StudentUpdateDto dto, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}


