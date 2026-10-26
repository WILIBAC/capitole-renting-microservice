using System;

namespace GtMotive.Estimate.Microservice.Domain.Entities
{
    /// <summary>
    /// Represents a vehicle in the renting fleet.
    /// </summary>
    public class Vehicle
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Vehicle"/> class.
        /// </summary>
        /// <param name="id">The unique identifier.</param>
        /// <param name="licensePlate">The vehicle license plate.</param>
        /// <param name="manufacturingDate">The manufacturing date.</param>
        public Vehicle(string id, string licensePlate, DateTime manufacturingDate)
        {
            if (manufacturingDate < DateTime.UtcNow.AddYears(-5))
            {
                throw new DomainException("The vehicle cannot be older than 5 years.");
            }

            Id = id;
            LicensePlate = licensePlate;
            ManufacturingDate = manufacturingDate;
            IsAvailable = true;
        }

        /// <summary>
        /// Gets the unique identifier of the vehicle.
        /// </summary>
        public string Id { get; private set; }

        /// <summary>
        /// Gets the license plate of the vehicle.
        /// </summary>
        public string LicensePlate { get; private set; }

        /// <summary>
        /// Gets the manufacturing date of the vehicle.
        /// </summary>
        public DateTime ManufacturingDate { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the vehicle is available for rent.
        /// </summary>
        public bool IsAvailable { get; private set; }

        /// <summary>
        /// Marks the vehicle as rented.
        /// </summary>
        public void Rent()
        {
            if (!IsAvailable)
            {
                throw new DomainException("The vehicle is not available for rent.");
            }

            IsAvailable = false;
        }

        /// <summary>
        /// Marks the vehicle as returned and available.
        /// </summary>
        public void Return()
        {
            IsAvailable = true;
        }
    }
}
