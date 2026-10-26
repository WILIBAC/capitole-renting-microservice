using System;
using GtMotive.Estimate.Microservice.Domain;
using GtMotive.Estimate.Microservice.Domain.Entities;
using Xunit;

namespace GtMotive.Estimate.Microservice.UnitTests
{
    /// <summary>
    /// Unit tests for the Vehicle entity.
    /// </summary>
    public class VehicleTests
    {
        /// <summary>
        /// Tests that a vehicle older than 5 years throws a DomainException.
        /// </summary>
        [Fact]
        public void ConstructorShouldThrowExceptionWhenVehicleIsOlderThan5Years()
        {
            // Arrange: Preparamos una fecha de hace 6 años
            var oldDate = DateTime.UtcNow.AddYears(-6);

            // Act & Assert: Verificamos que al intentar crearlo lance la excepción esperada
            var exception = Assert.Throws<DomainException>(() => new Vehicle("1", "ABC-123", oldDate));
            Assert.Equal("The vehicle cannot be older than 5 years.", exception.Message);
        }

        /// <summary>
        /// Tests that a valid vehicle is created successfully.
        /// </summary>
        [Fact]
        public void ConstructorShouldCreateVehicleWhenVehicleIsNewerThan5Years()
        {
            // Arrange: Preparamos una fecha válida de hace 2 años
            var validDate = DateTime.UtcNow.AddYears(-2);

            // Act: Creamos el vehículo
            var vehicle = new Vehicle("1", "ABC-123", validDate);

            // Assert: Verificamos que se creó correctamente y está disponible
            Assert.NotNull(vehicle);
            Assert.Equal("1", vehicle.Id);
            Assert.Equal("ABC-123", vehicle.LicensePlate);
            Assert.True(vehicle.IsAvailable);
        }
    }
}
