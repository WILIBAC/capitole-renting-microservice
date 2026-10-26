using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.Entities;
using GtMotive.Estimate.Microservice.Domain.Interfaces;

namespace GtMotive.Estimate.Microservice.FunctionalTests
{
    /// <summary>
    /// Fake repository for vehicles to isolate functional tests.
    /// </summary>
    public class FakeVehicleRepository : IVehicleRepository
    {
        /// <inheritdoc/>
        public Task AddAsync(Vehicle vehicle) => Task.CompletedTask;

        /// <inheritdoc/>
        public Task<IEnumerable<Vehicle>> GetAvailableVehiclesAsync() => Task.FromResult<IEnumerable<Vehicle>>(new List<Vehicle>());

        /// <inheritdoc/>
        public Task<Vehicle> GetByIdAsync(string id) => Task.FromResult(new Vehicle(id, "TEST-123", DateTime.UtcNow));

        /// <inheritdoc/>
        public Task UpdateAsync(Vehicle vehicle) => Task.CompletedTask;
    }
}
