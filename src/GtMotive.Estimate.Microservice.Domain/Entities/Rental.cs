using System;

namespace GtMotive.Estimate.Microservice.Domain.Entities
{
    /// <summary>
    /// Represents the rental of a vehicle by a customer.
    /// </summary>
    public class Rental
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Rental"/> class.
        /// </summary>
        /// <param name="id">The unique identifier of the rental.</param>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="vehicleId">The rented vehicle identifier.</param>
        public Rental(string id, string customerId, string vehicleId)
        {
            Id = id;
            CustomerId = customerId;
            VehicleId = vehicleId;
            StartDate = DateTime.UtcNow;
        }

        /// <summary>
        /// Gets the unique identifier of the rental.
        /// </summary>
        public string Id { get; private set; }

        /// <summary>
        /// Gets the customer identifier.
        /// </summary>
        public string CustomerId { get; private set; }

        /// <summary>
        /// Gets the rented vehicle identifier.
        /// </summary>
        public string VehicleId { get; private set; }

        /// <summary>
        /// Gets the start date of the rental.
        /// </summary>
        public DateTime StartDate { get; private set; }

        /// <summary>
        /// Gets the end date of the rental, if applicable.
        /// </summary>
        public DateTime? EndDate { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the rental is currently active.
        /// </summary>
        public bool IsActive => !EndDate.HasValue;

        /// <summary>
        /// Finishes the current rental.
        /// </summary>
        public void Finish()
        {
            if (!IsActive)
            {
                throw new DomainException("This rental is already finished.");
            }

            EndDate = DateTime.UtcNow;
        }
    }
}
