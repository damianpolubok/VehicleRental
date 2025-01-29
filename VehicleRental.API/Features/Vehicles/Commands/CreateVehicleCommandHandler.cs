using AutoMapper;
using MediatR;
using System.ComponentModel.DataAnnotations;
using VehicleRental.API.Models.Vehicles;
using VehicleRental.API.Repositories.Commands.ICommands;

namespace VehicleRental.API.Features.Vehicles.Commands
{
    public class CreateVehicleCommandHandler : IRequestHandler<CreateVehicleCommand, Vehicle>
    {
        private readonly IVehicleCommandRepository _vehicleCommandRepository;
        private readonly IMapper _mapper;

        public CreateVehicleCommandHandler(IVehicleCommandRepository vehicleCommandRepository, IMapper mapper)
        {
            _vehicleCommandRepository = vehicleCommandRepository;
            _mapper = mapper;
        }

        public async Task<Vehicle> Handle(CreateVehicleCommand request, CancellationToken cancellationToken)
        {
            object vehicleObject = request.VehicleType.ToLower() switch
            {
                "car" => _mapper.Map<Car>(request),
                "truck" => _mapper.Map<Truck>(request),
                "motorcycle" => _mapper.Map<Motorcycle>(request),
                _ => throw new ValidationException("Invalid Vehicle Type.")
            };

            var vehicle = (Vehicle)vehicleObject;

            await _vehicleCommandRepository.AddVehicleAsync(vehicle, cancellationToken);
            return vehicle;
        }
    }
}