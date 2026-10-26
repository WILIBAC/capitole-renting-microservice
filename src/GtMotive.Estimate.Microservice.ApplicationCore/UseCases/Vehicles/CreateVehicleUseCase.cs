using System;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.Entities;
using GtMotive.Estimate.Microservice.Domain.Interfaces;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicles
{
    /// <summary>
    /// Use case for creating a new vehicle.
    /// </summary>
    public class CreateVehicleUseCase
    {
        private readonly IVehicleRepository _vehicleRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateVehicleUseCase"/> class.
        /// </summary>
        /// <param name="vehicleRepository">The vehicle repository.</param>
        public CreateVehicleUseCase(IVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        /// <summary>
        /// Executes the use case to create a vehicle.
        /// </summary>
        /// <param name="licensePlate">The vehicle license plate.</param>
        /// <param name="manufacturingDate">The manufacturing date.</param>
        /// <returns>The generated vehicle identifier.</returns>
        public async Task<string> ExecuteAsync(string licensePlate, DateTime manufacturingDate)
        {
            var vehicleId = Guid.NewGuid().ToString();
            var vehicle = new Vehicle(vehicleId, licensePlate, manufacturingDate);

            await _vehicleRepository.AddAsync(vehicle);

            return vehicle.Id;
        }
    }
}
