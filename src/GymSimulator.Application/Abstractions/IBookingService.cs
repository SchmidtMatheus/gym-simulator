using GymSimulator.Application.DTOs;
using GymSimulator.Domain.Entities;

namespace GymSimulator.Application.Abstractions;

public interface IBookingService
{
    Task<IEnumerable<Booking>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Booking?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Booking> CreateAsync(BookingCreateDto dto, CancellationToken cancellationToken = default);
    Task<bool> CancelAsync(Guid id, string? reason = null, CancellationToken cancellationToken = default);
}


