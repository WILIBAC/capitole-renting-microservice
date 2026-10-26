namespace GtMotive.Estimate.Microservice.Api.Models
{
    /// <summary>
    /// Request model for renting a vehicle.
    /// </summary>
    public class RentVehicleRequest
    {
        /// <summary>
        /// Gets or sets the customer identifier.
        /// </summary>
        public string CustomerId { get; set; }

        /// <summary>
        /// Gets or sets the vehicle identifier.
        /// </summary>
        public string VehicleId { get; set; }
    }
}
