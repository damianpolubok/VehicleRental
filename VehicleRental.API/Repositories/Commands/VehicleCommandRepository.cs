using VehicleRental.API.Data;
using VehicleRental.API.Models.Vehicles;
using VehicleRental.API.Repositories.Commands.ICommands;

namespace VehicleRental.API.Repositories.Commands
{
    public class VehicleCommandRepository : IVehicleCommandRepository
    {
        private readonly VehicleRentalDbContext _context;

        public VehicleCommandRepository(VehicleRentalDbContext context)
        {
            _context = context;
        }

        public async Task AddVehicleAsync(Vehicle vehicle, CancellationToken cancellationToken)
        {
            _context.Vehicles.Add(vehicle);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<bool> DeleteVehicleAsync(int vehicleId, CancellationToken cancellationToken)
        {
            var vehicle = await _context.Vehicles.FindAsync(new object[] { vehicleId }, cancellationToken);
            if (vehicle == null)
            {
                return false;
            }

            _context.Vehicles.Remove(vehicle);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}