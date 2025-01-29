using MediatR;
using VehicleRental.API.Models.Fleet;
using VehicleRental.API.Repositories.Queries.IQueries;

namespace VehicleRental.API.Features.Fleet.Queries
{
    public class GetFleetReportQueryHandler : IRequestHandler<GetFleetReportQuery, FleetReport>
    {
        private readonly IFleetQueryRepository _fleetQueryRepository;

        public GetFleetReportQueryHandler(IFleetQueryRepository fleetQueryRepository)
        {
            _fleetQueryRepository = fleetQueryRepository;
        }

        public async Task<FleetReport> Handle(GetFleetReportQuery request, CancellationToken cancellationToken)
        {
            return await _fleetQueryRepository.GetFleetReportAsync(cancellationToken);
        }
    }
}