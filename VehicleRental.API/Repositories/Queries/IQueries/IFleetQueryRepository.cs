using VehicleRental.API.Models.Fleet;

namespace VehicleRental.API.Repositories.Queries.IQueries
{
    public interface IFleetQueryRepository
    {
        Task<FleetReport> GetFleetReportAsync(CancellationToken cancellationToken);
    }
}