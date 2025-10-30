using GymSimulator.Application.DTOs;
using GymSimulator.Application.DTOs.Common;

namespace GymSimulator.Application.Abstractions;

public interface IStudentService
{
    Task<PagedResponse<StudentDto>> GetAllAsync(PagedRequest request, CancellationToken cancellationToken = default);
    Task<StudentDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<StudentReportDto> GetStudentReportAsync(Guid studentId, CancellationToken cancellationToken);
    Task<StudentDto> CreateAsync(StudentCreateDto dto, CancellationToken cancellationToken = default);
    Task<StudentDto?> UpdateAsync(Guid id, StudentUpdateDto dto, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}


