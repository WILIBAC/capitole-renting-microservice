using System;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Api.Models;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicles;
using Microsoft.AspNetCore.Mvc;

namespace GtMotive.Estimate.Microservice.Api.Controllers
{
    /// <summary>
    /// Controller for managing vehicles.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class VehiclesController : ControllerBase
    {
        private readonly CreateVehicleUseCase _createVehicleUseCase;
        private readonly ListAvailableVehiclesUseCase _listAvailableVehiclesUseCase;

        /// <summary>
        /// Initializes a new instance of the <see cref="VehiclesController"/> class.
        /// </summary>
        /// <param name="createVehicleUseCase">The create vehicle use case.</param>
        /// <param name="listAvailableVehiclesUseCase">The list available vehicles use case.</param>
        public VehiclesController(CreateVehicleUseCase createVehicleUseCase, ListAvailableVehiclesUseCase listAvailableVehiclesUseCase)
        {
            _createVehicleUseCase = createVehicleUseCase ?? throw new ArgumentNullException(nameof(createVehicleUseCase));
            _listAvailableVehiclesUseCase = listAvailableVehiclesUseCase ?? throw new ArgumentNullException(nameof(listAvailableVehiclesUseCase));
        }

        /// <summary>
        /// Creates a new vehicle.
        /// </summary>
        /// <param name="request">The vehicle creation request.</param>
        /// <returns>The created vehicle ID.</returns>
        [HttpPost]
        public async Task<IActionResult> CreateVehicle([FromBody] CreateVehicleRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var vehicleId = await _createVehicleUseCase.ExecuteAsync(request.LicensePlate, request.ManufacturingDate);
            return Ok(new { Id = vehicleId });
        }

        /// <summary>
        /// Lists available vehicles.
        /// </summary>
        /// <returns>A list of available vehicles.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAvailableVehicles()
        {
            var vehicles = await _listAvailableVehiclesUseCase.ExecuteAsync();
            return Ok(vehicles);
        }
    }
}
