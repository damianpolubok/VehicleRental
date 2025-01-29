using VehicleRental.API.Models.Vehicles;

namespace VehicleRental.API.Repositories.Commands.ICommands
{
    public interface IVehicleCommandRepository
    {
        Task AddVehicleAsync(Vehicle vehicle, CancellationToken cancellationToken);
        Task<bool> DeleteVehicleAsync(int vehicleId, CancellationToken cancellationToken);
    }
}