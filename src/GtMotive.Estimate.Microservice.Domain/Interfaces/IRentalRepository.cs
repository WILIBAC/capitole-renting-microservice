using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.Entities;

namespace GtMotive.Estimate.Microservice.Domain.Interfaces
{
    /// <summary>
    /// Interface for the rental repository.
    /// </summary>
    public interface IRentalRepository
    {
        /// <summary>
        /// Registers a new rental asynchronously.
        /// </summary>
        /// <param name="rental">The rental to register.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task AddAsync(Rental rental);

        /// <summary>
        /// Retrieves the active rental for a specific customer.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <returns>The active rental found or null.</returns>
        Task<Rental> GetActiveRentalByCustomerIdAsync(string customerId);

        /// <summary>
        /// Retrieves a rental by its identifier.
        /// </summary>
        /// <param name="id">The rental identifier.</param>
        /// <returns>The found rental or null.</returns>
        Task<Rental> GetByIdAsync(string id);

        /// <summary>
        /// Updates the information of an existing rental.
        /// </summary>
        /// <param name="rental">The rental to update.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task UpdateAsync(Rental rental);
    }
}
