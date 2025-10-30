using GymSimulator.Application.DTOs;

namespace GymSimulator.Application.Mappings
{
    public static class BookingMapping
    {
        public static BookingDto ToDto(this Domain.Entities.Booking booking)
        {
            return new BookingDto
            {
                Id = booking.Id,
                StudentName = booking.Student?.Name ?? "N/A",
                ClassName = booking.Class?.ClassType?.Name ?? "N/A",
                BookingDate = booking.BookingDate,
                Status = booking.BookingStatus
            };
        }

        public static List<BookingDto> ToDto(this IEnumerable<Domain.Entities.Booking> bookings)
        {
            return bookings.Select(s => s.ToDto()).ToList();
        }
    }
}
