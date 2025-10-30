using GymSimulator.Domain.Enums;

namespace GymSimulator.Application.DTOs;

public class BookingDto
{
    public Guid Id { get; set; }
    public string StudentName { get; set; } = string.Empty;
    public string ClassName { get; set; } = string.Empty;
    public DateTime BookingDate { get; set; }
    public BookingStatus Status { get; set; }

}

public record BookingCreateDto(Guid StudentId, Guid ClassId);


