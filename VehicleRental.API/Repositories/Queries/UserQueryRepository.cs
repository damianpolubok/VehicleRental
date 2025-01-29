using Microsoft.AspNetCore.Identity;
using VehicleRental.API.Models.Users;
using VehicleRental.API.Repositories.Queries.IQueries;

namespace VehicleRental.API.Repositories.Queries
{
    public class UserQueryRepository : IUserQueryRepository
    {
        private readonly UserManager<User> _userManager;

        public UserQueryRepository(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> UserExistsAsync(string userId, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(userId);
            return user != null;
        }
    }
}