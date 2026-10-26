using System;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Api.Models;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Rentals;
using Microsoft.AspNetCore.Mvc;

namespace GtMotive.Estimate.Microservice.Api.Controllers
{
    /// <summary>
    /// Controller for managing rentals.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class RentalsController : ControllerBase
    {
        private readonly RentVehicleUseCase _rentVehicleUseCase;
        private readonly ReturnVehicleUseCase _returnVehicleUseCase;

        /// <summary>
        /// Initializes a new instance of the <see cref="RentalsController"/> class.
        /// </summary>
        /// <param name="rentVehicleUseCase">The rent vehicle use case.</param>
        /// <param name="returnVehicleUseCase">The return vehicle use case.</param>
        public RentalsController(RentVehicleUseCase rentVehicleUseCase, ReturnVehicleUseCase returnVehicleUseCase)
        {
            _rentVehicleUseCase = rentVehicleUseCase ?? throw new ArgumentNullException(nameof(rentVehicleUseCase));
            _returnVehicleUseCase = returnVehicleUseCase ?? throw new ArgumentNullException(nameof(returnVehicleUseCase));
        }

        /// <summary>
        /// Rents a vehicle for a customer.
        /// </summary>
        /// <param name="request">The rental request.</param>
        /// <returns>The rental ID.</returns>
        [HttpPost]
        public async Task<IActionResult> RentVehicle([FromBody] RentVehicleRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var rentalId = await _rentVehicleUseCase.ExecuteAsync(request.CustomerId, request.VehicleId);
            return Ok(new { Id = rentalId });
        }

        /// <summary>
        /// Returns a rented vehicle.
        /// </summary>
        /// <param name="rentalId">The rental ID to return.</param>
        /// <returns>An OK result.</returns>
        [HttpPut("{rentalId}/return")]
        public async Task<IActionResult> ReturnVehicle(string rentalId)
        {
            if (string.IsNullOrWhiteSpace(rentalId))
            {
                throw new ArgumentNullException(nameof(rentalId));
            }

            await _returnVehicleUseCase.ExecuteAsync(rentalId);
            return Ok();
        }
    }
}
