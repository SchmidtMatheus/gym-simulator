using GymSimulator.Application.DTOs;
using GymSimulator.Application.DTOs.Common;
using GymSimulator.Domain.Entities;

namespace GymSimulator.Application.Abstractions;

public interface IBookingService
{
    Task<PagedResponse<BookingDto>> GetAllAsync(PagedRequest request, CancellationToken cancellationToken = default);
    Task<BookingDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<BookingDto> CreateAsync(BookingCreateDto dto, CancellationToken cancellationToken = default);
    Task<bool> CancelAsync(Guid id, string? reason = null, CancellationToken cancellationToken = default);
}


