using MediatR;
using System.ComponentModel.DataAnnotations;
using VehicleRental.API.Models.Reservations;

namespace VehicleRental.API.Features.Reservations.Commands
{
    public class CreateReservationCommand : IRequest<Reservation>
    {
        [Required]
        public int VehicleId { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }
    }
}