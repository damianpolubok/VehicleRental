using VehicleRental.API.Models.Users;

namespace VehicleRental.API.Services.IServices
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}