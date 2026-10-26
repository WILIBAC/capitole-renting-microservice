using System;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain;
using GtMotive.Estimate.Microservice.Domain.Entities;
using GtMotive.Estimate.Microservice.Domain.Interfaces;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Rentals
{
    /// <summary>
    /// Use case for renting a vehicle.
    /// </summary>
    public class RentVehicleUseCase
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IRentalRepository _rentalRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="RentVehicleUseCase"/> class.
        /// </summary>
        /// <param name="vehicleRepository">The vehicle repository.</param>
        /// <param name="rentalRepository">The rental repository.</param>
        public RentVehicleUseCase(IVehicleRepository vehicleRepository, IRentalRepository rentalRepository)
        {
            _vehicleRepository = vehicleRepository;
            _rentalRepository = rentalRepository;
        }

        /// <summary>
        /// Executes the use case to rent a vehicle.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="vehicleId">The vehicle identifier.</param>
        /// <returns>The generated rental identifier.</returns>
        public async Task<string> ExecuteAsync(string customerId, string vehicleId)
        {
            var activeRental = await _rentalRepository.GetActiveRentalByCustomerIdAsync(customerId);
            if (activeRental != null)
            {
                throw new DomainException("The customer already has an active rental.");
            }

            var vehicle = await _vehicleRepository.GetByIdAsync(vehicleId) ?? throw new DomainException("The vehicle does not exist.");

            // Cambia el estado del vehículo a "no disponible" (aplica regla de dominio)
            vehicle.Rent();

            var rentalId = Guid.NewGuid().ToString();
            var rental = new Rental(rentalId, customerId, vehicleId);

            await _vehicleRepository.UpdateAsync(vehicle);
            await _rentalRepository.AddAsync(rental);

            return rental.Id;
        }
    }
}
