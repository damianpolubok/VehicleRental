using MediatR;
using System.ComponentModel.DataAnnotations;

namespace VehicleRental.API.Features.Users.Commands
{
    public class RegisterUserCommand : IRequest<Unit>
    {
        [Required]
        public required string Email { get; set; }
        [Required]
        public required string Password { get; set; }
    }
}