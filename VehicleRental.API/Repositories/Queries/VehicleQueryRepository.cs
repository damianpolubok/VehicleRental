using Microsoft.EntityFrameworkCore;
using VehicleRental.API.Data;
using VehicleRental.API.Models.Vehicles;
using VehicleRental.API.Repositories.Queries.IQueries;

namespace VehicleRental.API.Repositories.Queries
{
    public class VehicleQueryRepository : IVehicleQueryRepository
    {
        private readonly VehicleRentalDbContext _context;

        public VehicleQueryRepository(VehicleRentalDbContext context)
        {
            _context = context;
        }

        public async Task<Vehicle?> GetVehicleByIdAsync(int vehicleId, CancellationToken cancellationToken)
        {
            return await _context.Vehicles.AsNoTracking()
                .FirstOrDefaultAsync(v => v.Id == vehicleId, cancellationToken);
        }

        public async Task<IEnumerable<Vehicle>> GetAvailableVehiclesAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken)
        {
            return await _context.Vehicles
                .Where(v => !_context.Reservations.Any(r =>
                    r.VehicleId == v.Id &&
                    r.IsActive &&
                    r.StartDate < endDate &&
                    r.EndDate > startDate))
                .ToListAsync(cancellationToken);
        }
    }
}