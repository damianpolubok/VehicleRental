using MediatR;
using System.Security.Claims;
using VehicleRental.API.Models.Reservations;
using VehicleRental.API.Repositories.Queries.IQueries;

namespace VehicleRental.API.Features.Reservations.Queries
{
    public class GetUserReservationsQueryHandler : IRequestHandler<GetUserReservationsQuery, IEnumerable<Reservation>>
    {
        private readonly IReservationQueryRepository _reservationQueryRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetUserReservationsQueryHandler(
            IReservationQueryRepository reservationQueryRepository,
            IHttpContextAccessor httpContextAccessor)
        {
            _reservationQueryRepository = reservationQueryRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IEnumerable<Reservation>> Handle(GetUserReservationsQuery request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                throw new UnauthorizedAccessException("You must be logged in to view your reservations.");
            }

            return await _reservationQueryRepository.GetReservationsByUserIdAsync(userId, cancellationToken);
        }
    }
}