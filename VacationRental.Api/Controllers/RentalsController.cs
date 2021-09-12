using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using VacationRental.Api.Domain.Booking;
using VacationRental.Api.Domain.Resources;
using VacationRental.Api.Domain.Rental;

namespace VacationRental.Api.Controllers
{
    [Route("api/v1/rentals")]
    [ApiController]
    public class RentalsController : ControllerBase
    {
        private readonly IDictionary<int, RentalViewDTO> _rentals;
        private readonly IDictionary<int, BookingViewDTO> _bookings;


        public RentalsController(IDictionary<int, RentalViewDTO> rentals, IDictionary<int, BookingViewDTO> bookings)
        {
            _rentals = rentals;
            _bookings = bookings;
        }

        [HttpGet]
        [Route("{rentalId:int}")]
        public RentalViewDTO Get(int rentalId)
        {
            if (!_rentals.ContainsKey(rentalId))
                throw new ApplicationException("Rental not found");

            return _rentals[rentalId];
        }

        [HttpPost]
        public ResourceIdViewModel Post(RentalBindingDTO model)
        {
            var key = new ResourceIdViewModel { Id = _rentals.Keys.Count + 1 };

            _rentals.Add(key.Id, new RentalViewDTO
            {
                Id = key.Id,
                Units = model.Units,
                PreparationTimeInDays = model.PreparationTimeInDays
            });

            return key;
        }

        [HttpPut]
        [Route("{rentalId:int}")]
        public RentalViewDTO Put(int rentalId, [FromBody] RentalBindingDTO model)
        {
            if (!_rentals.ContainsKey(rentalId))
                throw new ApplicationException("Rental not found");

            var rental = _rentals[rentalId];
            rental.PreparationTimeInDays = model.PreparationTimeInDays;
            rental.Units = model.Units;

            var bookings = _bookings.Where(x => x.Value.RentalId == rental.Id).ToList();
            bookings.ForEach(booking =>
            {
                booking.Value.PreparationTimeInDays = model.PreparationTimeInDays;
            });

            return _rentals[rentalId];
        }
    }
}
