using AutoMapper;
using VehicleRental.API.Features.Users.Commands;
using VehicleRental.API.Models.Users;

namespace VehicleRental.API.Services
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<RegisterRequest, RegisterUserCommand>();
            CreateMap<RegisterUserCommand, User>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));
        }
    }
}