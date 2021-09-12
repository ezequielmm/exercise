using System;

namespace VacationRental.Api.Domain.Booking
{
    public class BookingViewDTO
    {
        public int Id { get; set; }
        public int RentalId { get; set; }
        public DateTime Start { get; set; }
        public int Nights { get; set; }
        public int PreparationTimeInDays { get; set; }

    }
}
