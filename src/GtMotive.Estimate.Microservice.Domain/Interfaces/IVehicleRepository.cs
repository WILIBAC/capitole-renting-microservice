using System.Collections.Generic;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.Entities;

namespace GtMotive.Estimate.Microservice.Domain.Interfaces
{
    /// <summary>
    /// Interface for the vehicle repository.
    /// </summary>
    public interface IVehicleRepository
    {
        /// <summary>
        /// Adds a new vehicle asynchronously.
        /// </summary>
        /// <param name="vehicle">The vehicle to add.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task AddAsync(Vehicle vehicle);

        /// <summary>
        /// Retrieves the vehicles available for rent.
        /// </summary>
        /// <returns>A list of available vehicles.</returns>
        Task<IEnumerable<Vehicle>> GetAvailableVehiclesAsync();

        /// <summary>
        /// Retrieves a vehicle by its identifier.
        /// </summary>
        /// <param name="id">The vehicle identifier.</param>
        /// <returns>The found vehicle or null.</returns>
        Task<Vehicle> GetByIdAsync(string id);

        /// <summary>
        /// Updates the information of an existing vehicle.
        /// </summary>
        /// <param name="vehicle">The vehicle to update.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task UpdateAsync(Vehicle vehicle);
    }
}
