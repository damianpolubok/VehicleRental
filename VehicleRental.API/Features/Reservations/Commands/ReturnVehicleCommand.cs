using MediatR;
using System.ComponentModel.DataAnnotations;

namespace VehicleRental.API.Features.Reservations.Commands
{
    public class ReturnVehicleCommand : IRequest<Unit>
    {
        [Required]
        public int ReservationId { get; set; }
    }
}