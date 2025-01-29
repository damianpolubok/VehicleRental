using Microsoft.AspNetCore.Identity;
using VehicleRental.API.Models.Reservations;

namespace VehicleRental.API.Models.Users
{
    public class User : IdentityUser
    {
        public string? Name { get; set; }
        public ICollection<Reservation>? Reservations { get; set; }
    }
}