using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain;
using GtMotive.Estimate.Microservice.Domain.Interfaces;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Rentals
{
    /// <summary>
    /// Use case for returning a rented vehicle.
    /// </summary>
    public class ReturnVehicleUseCase
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IRentalRepository _rentalRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReturnVehicleUseCase"/> class.
        /// </summary>
        /// <param name="vehicleRepository">The vehicle repository.</param>
        /// <param name="rentalRepository">The rental repository.</param>
        public ReturnVehicleUseCase(IVehicleRepository vehicleRepository, IRentalRepository rentalRepository)
        {
            _vehicleRepository = vehicleRepository;
            _rentalRepository = rentalRepository;
        }

        /// <summary>
        /// Executes the use case to return a vehicle.
        /// </summary>
        /// <param name="rentalId">The rental identifier.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public async Task ExecuteAsync(string rentalId)
        {
            var rental = await _rentalRepository.GetByIdAsync(rentalId) ?? throw new DomainException("The rental does not exist.");

            // Marca el alquiler como finalizado y asigna la fecha de fin
            rental.Finish();

            var vehicle = await _vehicleRepository.GetByIdAsync(rental.VehicleId);
            if (vehicle != null)
            {
                // Vuelve a marcar el vehículo como disponible
                vehicle.Return();
                await _vehicleRepository.UpdateAsync(vehicle);
            }

            await _rentalRepository.UpdateAsync(rental);
        }
    }
}
