using MediatR;

namespace VehicleRental.API.Features.Reservations.Queries
{
    public class GetAvailableVehiclesQuery : IRequest<IEnumerable<AvailableVehicleDto>>
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}