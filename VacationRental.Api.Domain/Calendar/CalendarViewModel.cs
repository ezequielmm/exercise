﻿using System.Collections.Generic;

namespace VacationRental.Api.Domain.Calendar
{
    public class CalendarViewModel
    {
        public int RentalId { get; set; }
        public List<CalendarDateViewDTO> Dates { get; set; }
        public int PreparationTimeInDays { get; set; }

    }
}
