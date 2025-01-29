using AutoMapper;
using MediatR;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using VehicleRental.API.Models.Reservations;
using VehicleRental.API.Repositories.Commands.ICommands;
using VehicleRental.API.Repositories.Queries.IQueries;
using VehicleRental.API.Services.IServices;

namespace VehicleRental.API.Features.Reservations.Commands
{
    public class CreateReservationCommandHandler : IRequestHandler<CreateReservationCommand, Reservation>
    {
        private readonly IVehicleQueryRepository _vehicleQueryRepository;
        private readonly IReservationQueryRepository _reservationQueryRepository;
        private readonly IReservationCommandRepository _reservationCommandRepository;
        private readonly IReservationPricingService _pricingService;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CreateReservationCommandHandler(
            IVehicleQueryRepository vehicleQueryRepository,
            IReservationQueryRepository reservationQueryRepository,
            IReservationCommandRepository reservationCommandRepository,
            IReservationPricingService pricingService,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor)
        {
            _vehicleQueryRepository = vehicleQueryRepository;
            _reservationQueryRepository = reservationQueryRepository;
            _reservationCommandRepository = reservationCommandRepository;
            _pricingService = pricingService;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Reservation> Handle(CreateReservationCommand request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                throw new UnauthorizedAccessException("You must be logged in to create a reservation.");
            }

            if (request.StartDate >= request.EndDate)
            {
                throw new ValidationException("Start date must be earlier than end date.");
            }

            if (request.StartDate < DateTime.UtcNow)
            {
                throw new ValidationException("Start date cannot be in the past.");
            }

            var vehicle = await _vehicleQueryRepository.GetVehicleByIdAsync(request.VehicleId, cancellationToken);
            if (vehicle == null)
            {
                throw new KeyNotFoundException("Vehicle not found.");
            }

            var isOverlapping = await _reservationQueryRepository.IsVehicleReservedAsync(
                request.VehicleId, request.StartDate, request.EndDate, null, cancellationToken);
            if (isOverlapping)
            {
                throw new InvalidOperationException("The vehicle is already reserved for the specified time period.");
            }

            var totalPrice = _pricingService.CalculateTotalPrice(request.StartDate, request.EndDate, vehicle.RentalPricePerMinute);

            var reservation = _mapper.Map<Reservation>(request);
            reservation.UserId = userId;
            reservation.TotalPrice = totalPrice;

            await _reservationCommandRepository.AddReservationAsync(reservation, cancellationToken);

            var createdReservation = await _reservationQueryRepository.GetReservationByIdAsync(reservation.Id, cancellationToken);
            if (createdReservation == null)
            {
                throw new KeyNotFoundException($"Reservation with ID {reservation.Id} was not found after creation.");
            }

            return createdReservation;
        }
    }
}