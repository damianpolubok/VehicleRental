using MediatR;
using System.Security.Claims;
using VehicleRental.API.Repositories.Commands.ICommands;
using VehicleRental.API.Repositories.Queries.IQueries;

namespace VehicleRental.API.Features.Reservations.Commands
{
    public class ReturnVehicleCommandHandler : IRequestHandler<ReturnVehicleCommand, Unit>
    {
        private readonly IReservationCommandRepository _reservationCommandRepository;
        private readonly IReservationQueryRepository _reservationQueryRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ReturnVehicleCommandHandler(
            IReservationCommandRepository reservationCommandRepository,
            IReservationQueryRepository reservationQueryRepository,
            IHttpContextAccessor httpContextAccessor)
        {
            _reservationCommandRepository = reservationCommandRepository;
            _reservationQueryRepository = reservationQueryRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Unit> Handle(ReturnVehicleCommand request, CancellationToken cancellationToken)
        {
            var reservation = await _reservationQueryRepository.GetReservationByIdAsync(request.ReservationId, cancellationToken);

            if (reservation == null)
            {
                throw new KeyNotFoundException("Reservation not found.");
            }

            var currentUserId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(currentUserId) || currentUserId != reservation.UserId)
            {
                throw new UnauthorizedAccessException("You are not authorized to return this vehicle.");
            }

            if (!reservation.IsActive)
            {
                throw new InvalidOperationException("Reservation is already completed.");
            }

            reservation.IsActive = false;
            await _reservationCommandRepository.UpdateReservationAsync(reservation, cancellationToken);

            return Unit.Value;
        }
    }
}