using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using VacationRental.Api.Domain.Booking;
using VacationRental.Api.Domain.Resources;
using VacationRental.Api.Domain.Rental;

namespace VacationRental.Api.Controllers
{
    [Route("api/v1/bookings")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly IDictionary<int, RentalViewDTO> _rentals;
        private readonly IDictionary<int, BookingViewDTO> _bookings;

        public BookingsController(
            IDictionary<int, RentalViewDTO> rentals,
            IDictionary<int, BookingViewDTO> bookings)
        {
            _rentals = rentals;
            _bookings = bookings;
        }

        [HttpGet]
        [Route("{bookingId:int}")]
        public BookingViewDTO Get(int bookingId)
        {
            if (!_bookings.ContainsKey(bookingId))
                throw new ApplicationException("Booking not found");

            return _bookings[bookingId];
        }

        [HttpPost]
        public ResourceIdViewModel Post(BookingBindingDTO model)
        {
            var key = new ResourceIdViewModel { Id = _bookings.Keys.Count + 1 };

            _bookings.Add(key.Id, new BookingViewDTO
            {
                Id = key.Id,
                Start = model.Start.Date,
                PreparationTimeInDays = _rentals[model.RentalId].PreparationTimeInDays,
                Nights = model.Nights,
                RentalId = model.RentalId
            });

            return key;
        }
    }
}
