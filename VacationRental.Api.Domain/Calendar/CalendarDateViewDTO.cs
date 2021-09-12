using System;
using System.Collections.Generic;

namespace VacationRental.Api.Domain.Calendar
{
    public class CalendarDateViewDTO
    {
        public DateTime Date { get; set; }
        public List<CalendarBookingViewDTO> Bookings { get; set; }
        public int Unit { get; set; }

    }
}
