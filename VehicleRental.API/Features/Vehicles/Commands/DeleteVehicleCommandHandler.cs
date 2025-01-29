using MediatR;
using VehicleRental.API.Repositories.Commands.ICommands;

namespace VehicleRental.API.Features.Vehicles.Commands
{
    public class DeleteVehicleCommandHandler : IRequestHandler<DeleteVehicleCommand, Unit>
    {
        private readonly IVehicleCommandRepository _vehicleCommandRepository;

        public DeleteVehicleCommandHandler(IVehicleCommandRepository vehicleCommandRepository)
        {
            _vehicleCommandRepository = vehicleCommandRepository;
        }

        public async Task<Unit> Handle(DeleteVehicleCommand request, CancellationToken cancellationToken)
        {
            var success = await _vehicleCommandRepository.DeleteVehicleAsync(request.VehicleId, cancellationToken);

            if (!success)
            {
                throw new KeyNotFoundException($"Vehicle with ID {request.VehicleId} not found.");
            }

            return Unit.Value;
        }
    }
}