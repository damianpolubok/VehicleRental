using VehicleRental.API.Models.Vehicles;

namespace VehicleRental.API.Repositories.Queries.IQueries
{
    public interface IVehicleQueryRepository
    {
        Task<IEnumerable<Vehicle>> GetAvailableVehiclesAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken);
        Task<Vehicle?> GetVehicleByIdAsync(int vehicleId, CancellationToken cancellationToken);
    }
}