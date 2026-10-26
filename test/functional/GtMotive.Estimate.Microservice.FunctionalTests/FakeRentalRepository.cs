using System.Collections.Generic;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.Entities;
using GtMotive.Estimate.Microservice.Domain.Interfaces;

namespace GtMotive.Estimate.Microservice.FunctionalTests
{
    /// <summary>
    /// Fake repository for rentals to isolate functional tests.
    /// </summary>
    public class FakeRentalRepository : IRentalRepository
    {
        private readonly List<Rental> _rentals = new();

        /// <inheritdoc/>
        public Task AddAsync(Rental rental)
        {
            _rentals.Add(rental);
            return Task.CompletedTask;
        }

        /// <inheritdoc/>
        public Task<Rental> GetActiveRentalByCustomerIdAsync(string customerId)
        {
            var rental = _rentals.Find(r => r.CustomerId == customerId && r.IsActive);
            return Task.FromResult(rental);
        }

        /// <inheritdoc/>
        public Task<Rental> GetByIdAsync(string id) => Task.FromResult<Rental>(null);

        /// <inheritdoc/>
        public Task UpdateAsync(Rental rental) => Task.CompletedTask;
    }
}
