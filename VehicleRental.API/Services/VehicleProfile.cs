using AutoMapper;
using VehicleRental.API.Features.Vehicles.Commands;
using VehicleRental.API.Features.Reservations.Queries;
using VehicleRental.API.Models.Vehicles;

namespace VehicleRental.API.MappingProfiles
{
    public class VehicleProfile : Profile
    {
        public VehicleProfile()
        {
            CreateMap<Vehicle, AvailableVehicleDto>()
                .ForMember(dest => dest.VehicleType, opt => opt.MapFrom(src => src.GetType().Name));

            CreateMap<Car, AvailableVehicleDto>()
                .IncludeBase<Vehicle, AvailableVehicleDto>()
                .ForMember(dest => dest.Seats, opt => opt.MapFrom(src => src.Seats))
                .ForMember(dest => dest.HasAirConditioning, opt => opt.MapFrom(src => src.HasAirConditioning));

            CreateMap<Truck, AvailableVehicleDto>()
                .IncludeBase<Vehicle, AvailableVehicleDto>()
                .ForMember(dest => dest.CargoCapacity, opt => opt.MapFrom(src => src.CargoCapacity))
                .ForMember(dest => dest.Axles, opt => opt.MapFrom(src => src.Axles))
                .ForMember(dest => dest.HasAirConditioning, opt => opt.MapFrom(src => src.HasAirConditioning));

            CreateMap<Motorcycle, AvailableVehicleDto>()
                .IncludeBase<Vehicle, AvailableVehicleDto>()
                .ForMember(dest => dest.HasSidecar, opt => opt.MapFrom(src => src.HasSidecar));

            CreateMap<CreateVehicleCommand, Car>()
                .ForMember(dest => dest.Seats, opt => opt.MapFrom(src => src.Seats ?? 4))
                .ForMember(dest => dest.HasAirConditioning, opt => opt.MapFrom(src => src.HasAirConditioning ?? false));

            CreateMap<CreateVehicleCommand, Truck>()
                .ForMember(dest => dest.CargoCapacity, opt => opt.MapFrom(src => src.CargoCapacity ?? 1000))
                .ForMember(dest => dest.Axles, opt => opt.MapFrom(src => src.Axles ?? 2))
                .ForMember(dest => dest.HasAirConditioning, opt => opt.MapFrom(src => src.HasAirConditioning ?? false));

            CreateMap<CreateVehicleCommand, Motorcycle>()
                .ForMember(dest => dest.HasSidecar, opt => opt.MapFrom(src => src.HasSidecar ?? false));
        }
    }
}