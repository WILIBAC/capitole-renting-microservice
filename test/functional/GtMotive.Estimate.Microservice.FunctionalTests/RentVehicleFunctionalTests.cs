using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Rentals;
using GtMotive.Estimate.Microservice.Domain;
using GtMotive.Estimate.Microservice.Domain.Entities;
using Xunit;

namespace GtMotive.Estimate.Microservice.FunctionalTests
{
    /// <summary>
    /// Functional tests for rental use cases.
    /// </summary>
    public class RentVehicleFunctionalTests
    {
        /// <summary>
        /// Tests that a customer cannot rent more than one vehicle at a time.
        /// </summary>
        [Fact]
        public async Task ExecuteAsyncShouldThrowExceptionWhenCustomerHasActiveRental()
        {
            // Arrange: Preparamos los repositorios en memoria y el caso de uso
            var vehicleRepo = new FakeVehicleRepository();
            var rentalRepo = new FakeRentalRepository();
            var useCase = new RentVehicleUseCase(vehicleRepo, rentalRepo);

            var customerId = "customer-1";
            var vehicleId = "vehicle-new";

            // Simulamos que el cliente ya tiene un alquiler activo en el sistema
            var existingRental = new Rental("rental-1", customerId, "vehicle-old");
            await rentalRepo.AddAsync(existingRental);

            // Act & Assert: Verificamos que intentar alquilar otro vehículo lance excepción
            var exception = await Assert.ThrowsAsync<DomainException>(() => useCase.ExecuteAsync(customerId, vehicleId));
            Assert.Equal("The customer already has an active rental.", exception.Message);
        }
    }
}
