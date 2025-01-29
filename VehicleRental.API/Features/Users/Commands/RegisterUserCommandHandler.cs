using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using VehicleRental.API.Models.Users;

namespace VehicleRental.API.Features.Users.Commands
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Unit>
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public RegisterUserCommandHandler(UserManager<User> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<User>(request);

            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
            {
                var identityErrors = result.Errors
                    .GroupBy(e => e.Code)
                    .ToDictionary(g => g.Key, g => g.Select(e => e.Description).ToArray());

                throw new ValidationException($"User registration failed: {string.Join(", ", result.Errors.Select(e => e.Description))}");
            }

            return Unit.Value;
        }
    }
}