using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.Entities;
using GtMotive.Estimate.Microservice.Domain.Interfaces;
using MongoDB.Driver;

namespace GtMotive.Estimate.Microservice.Infrastructure.MongoDb
{
    /// <summary>
    /// Repository for managing vehicles in MongoDB.
    /// </summary>
    public class VehicleRepository : IVehicleRepository
    {
        private readonly IMongoCollection<Vehicle> _collection;

        /// <summary>
        /// Initializes a new instance of the <see cref="VehicleRepository"/> class.
        /// </summary>
        /// <param name="mongoDatabase">The MongoDB database instance.</param>
        public VehicleRepository(IMongoDatabase mongoDatabase)
        {
            if (mongoDatabase == null)
            {
                throw new ArgumentNullException(nameof(mongoDatabase));
            }

            _collection = mongoDatabase.GetCollection<Vehicle>("Vehicles");
        }

        /// <summary>
        /// Adds a new vehicle asynchronously.
        /// </summary>
        /// <param name="vehicle">The vehicle to add.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public async Task AddAsync(Vehicle vehicle)
        {
            await _collection.InsertOneAsync(vehicle);
        }

        /// <summary>
        /// Retrieves the vehicles available for rent.
        /// </summary>
        /// <returns>A list of available vehicles.</returns>
        public async Task<IEnumerable<Vehicle>> GetAvailableVehiclesAsync()
        {
            return await _collection.Find(v => v.IsAvailable).ToListAsync();
        }

        /// <summary>
        /// Retrieves a vehicle by its identifier.
        /// </summary>
        /// <param name="id">The vehicle identifier.</param>
        /// <returns>The found vehicle or null.</returns>
        public async Task<Vehicle> GetByIdAsync(string id)
        {
            return await _collection.Find(v => v.Id == id).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Updates the information of an existing vehicle.
        /// </summary>
        /// <param name="vehicle">The vehicle to update.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public async Task UpdateAsync(Vehicle vehicle)
        {
            await _collection.ReplaceOneAsync(v => v.Id == vehicle.Id, vehicle);
        }
    }
}
