using System;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.Entities;
using GtMotive.Estimate.Microservice.Domain.Interfaces;
using MongoDB.Driver;

namespace GtMotive.Estimate.Microservice.Infrastructure.MongoDb
{
    /// <summary>
    /// Repository for managing rentals in MongoDB.
    /// </summary>
    public class RentalRepository : IRentalRepository
    {
        private readonly IMongoCollection<Rental> _collection;

        /// <summary>
        /// Initializes a new instance of the <see cref="RentalRepository"/> class.
        /// </summary>
        /// <param name="mongoDatabase">The MongoDB database instance.</param>
        public RentalRepository(IMongoDatabase mongoDatabase)
        {
            if (mongoDatabase == null)
            {
                throw new ArgumentNullException(nameof(mongoDatabase));
            }

            _collection = mongoDatabase.GetCollection<Rental>("Rentals");
        }

        /// <summary>
        /// Registers a new rental asynchronously.
        /// </summary>
        /// <param name="rental">The rental to register.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public async Task AddAsync(Rental rental)
        {
            await _collection.InsertOneAsync(rental);
        }

        /// <summary>
        /// Retrieves the active rental for a specific customer.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <returns>The active rental found or null.</returns>
        public async Task<Rental> GetActiveRentalByCustomerIdAsync(string customerId)
        {
            return await _collection.Find(r => r.CustomerId == customerId && r.EndDate == null).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Retrieves a rental by its identifier.
        /// </summary>
        /// <param name="id">The rental identifier.</param>
        /// <returns>The found rental or null.</returns>
        public async Task<Rental> GetByIdAsync(string id)
        {
            return await _collection.Find(r => r.Id == id).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Updates the information of an existing rental.
        /// </summary>
        /// <param name="rental">The rental to update.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public async Task UpdateAsync(Rental rental)
        {
            await _collection.ReplaceOneAsync(r => r.Id == rental.Id, rental);
        }
    }
}
