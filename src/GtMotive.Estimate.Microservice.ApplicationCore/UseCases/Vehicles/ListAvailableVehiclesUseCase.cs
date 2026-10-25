using System.Collections.Generic;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.Entities;
using GtMotive.Estimate.Microservice.Domain.Interfaces;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicles
{
    /// <summary>
    /// Use case for listing available vehicles.
    /// </summary>
    public class ListAvailableVehiclesUseCase
    {
        private readonly IVehicleRepository _vehicleRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ListAvailableVehiclesUseCase"/> class.
        /// </summary>
        /// <param name="vehicleRepository">The vehicle repository.</param>
        public ListAvailableVehiclesUseCase(IVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        /// <summary>
        /// Executes the use case to retrieve available vehicles.
        /// </summary>
        /// <returns>A collection of available vehicles.</returns>
        public async Task<IEnumerable<Vehicle>> ExecuteAsync()
        {
            return await _vehicleRepository.GetAvailableVehiclesAsync();
        }
    }
}
