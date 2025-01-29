using MediatR;
using VehicleRental.API.Models.Reservations;

namespace VehicleRental.API.Features.Reservations.Queries
{
    public class GetUserReservationsQuery : IRequest<IEnumerable<Reservation>>
    {
    }
}