using MediatR;
using VehicleRental.API.Models.Fleet;

namespace VehicleRental.API.Features.Fleet.Queries
{
    public class GetFleetReportQuery : IRequest<FleetReport>
    {
    }
}