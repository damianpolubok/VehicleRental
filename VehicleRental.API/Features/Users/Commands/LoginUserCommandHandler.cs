using MediatR;
using Microsoft.AspNetCore.Identity;
using VehicleRental.API.Models.Users;
using VehicleRental.API.Services.IServices;

namespace VehicleRental.API.Features.Users.Commands
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, string>
    {
        private readonly UserManager<User> _userManager;
        private readonly ITokenService _tokenService;

        public LoginUserCommandHandler(UserManager<User> userManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }

        public async Task<string> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, request.Password))
            {
                throw new UnauthorizedAccessException("Invalid credentials.");
            }

            return _tokenService.GenerateToken(user);
        }
    }
}