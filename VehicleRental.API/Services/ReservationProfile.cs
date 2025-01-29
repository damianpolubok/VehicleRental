using AutoMapper;
using VehicleRental.API.Features.Reservations.Commands;
using VehicleRental.API.Models.Reservations;

namespace VehicleRental.API.Services
{
    public class ReservationProfile : Profile
    {
        public ReservationProfile()
        {
            CreateMap<CreateReservationCommand, Reservation>()
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true));
        }
    }
}