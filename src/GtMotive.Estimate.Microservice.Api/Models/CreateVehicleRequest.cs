using System;

namespace GtMotive.Estimate.Microservice.Api.Models
{
    /// <summary>
    /// Request model for creating a vehicle.
    /// </summary>
    public class CreateVehicleRequest
    {
        /// <summary>
        /// Gets or sets the license plate.
        /// </summary>
        public string LicensePlate { get; set; }

        /// <summary>
        /// Gets or sets the manufacturing date.
        /// </summary>
        public DateTime ManufacturingDate { get; set; }
    }
}
