using System;
using System.Collections.Generic;

namespace VacationRental.Api.Domain.Calendar
{
    public class CalendarDateViewModel
    {
        public DateTime Date { get; set; }
        public List<CalendarBookingViewModel> Bookings { get; set; }
        public int Unit { get; set; }

    }
}
