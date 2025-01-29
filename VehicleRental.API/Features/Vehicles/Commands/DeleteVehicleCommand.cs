using MediatR;
using System.ComponentModel.DataAnnotations;

namespace VehicleRental.API.Features.Vehicles.Commands
{
    public class DeleteVehicleCommand : IRequest<Unit>
    {
        [Required]
        public int VehicleId { get; set; }
    }
}