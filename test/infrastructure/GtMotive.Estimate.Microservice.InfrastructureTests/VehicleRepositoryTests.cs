using System;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.Entities;
using GtMotive.Estimate.Microservice.Infrastructure.MongoDb;
using MongoDB.Driver;
using Moq;
using Xunit;

namespace GtMotive.Estimate.Microservice.InfrastructureTests
{
    /// <summary>
    /// Infrastructure tests for the VehicleRepository.
    /// </summary>
    public class VehicleRepositoryTests
    {
        /// <summary>
        /// Tests that adding a vehicle calls the insert method on the MongoDB collection.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.</returns>
        [Fact]
        public async Task AddAsyncShouldInsertVehicleIntoCollection()
        {
            // Arrange: Simulamos la base de datos y la colección de Mongo
            var mockCollection = new Mock<IMongoCollection<Vehicle>>();
            var mockDatabase = new Mock<IMongoDatabase>();

            mockDatabase.Setup(db => db.GetCollection<Vehicle>("Vehicles", null))
                        .Returns(mockCollection.Object);

            var repository = new VehicleRepository(mockDatabase.Object);
            var vehicle = new Vehicle("1", "TEST-123", DateTime.UtcNow);

            // Act: Llamamos a nuestro método de infraestructura
            await repository.AddAsync(vehicle);

            // Assert: Verificamos que nuestro repositorio realmente haya invocado el método InsertOneAsync de Mongo
            mockCollection.Verify(
                c => c.InsertOneAsync(vehicle, null, default),
                Times.Once);
        }
    }
}
