using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using VacationRental.Api.Domain.Booking;
using VacationRental.Api.Domain.Calendar;
using VacationRental.Api.Domain.Rental;

namespace VacationRental.Api.Controllers
{
    [Route("api/v1/calendar")]
    [ApiController]
    public class CalendarController : ControllerBase
    {
        private readonly IDictionary<int, RentalViewDTO> _rentals;
        private readonly IDictionary<int, BookingViewDTO> _bookings;

        public CalendarController(
            IDictionary<int, RentalViewDTO> rentals,
            IDictionary<int, BookingViewDTO> bookings)
        {
            _rentals = rentals;
            _bookings = bookings;
        }

        [HttpGet]
        public CalendarViewModel Get(int rentalId, DateTime start, int nights)
        {
            var result = new CalendarViewModel
            {
                RentalId = rentalId,
                Dates = new List<CalendarDateViewDTO>()
            };
            int units = _rentals[rentalId].Units;

            for (var i = 0; i < nights; i++)
            {
                int reservedUnits = 0;
                var date = new CalendarDateViewDTO
                {
                    Date = start.Date.AddDays(i),
                    Bookings = new List<CalendarBookingViewDTO>()
                };
                var rentalBooking = _bookings.Values.Where(x => x.RentalId == rentalId).ToList();

                foreach (var booking in rentalBooking)
                {
                    DateTime bookingEnding = booking.Start.AddDays(booking.Nights);
                    DateTime bookingStarting = booking.Start;

                    if (bookingStarting <= date.Date && bookingEnding >= date.Date)
                    {
                        reservedUnits += 1;
                        date.Bookings.Add(new CalendarBookingViewDTO
                        {
                            Id = booking.Id,
                            Nights = booking.Nights
                        });
                    }
                }
                date.Unit = units - reservedUnits;
                result.Dates.Add(date);
            }
            result.PreparationTimeInDays = _rentals[rentalId].PreparationTimeInDays;
            return result;
        }
    }
}
