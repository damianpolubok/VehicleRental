using Microsoft.EntityFrameworkCore;
using VehicleRental.API.Data;
using VehicleRental.API.Models.Fleet;
using VehicleRental.API.Repositories.Queries.IQueries;

namespace VehicleRental.API.Repositories.Queries
{
    public class FleetQueryRepository : IFleetQueryRepository
    {
        private readonly VehicleRentalDbContext _context;

        public FleetQueryRepository(VehicleRentalDbContext context)
        {
            _context = context;
        }

        public async Task<FleetReport> GetFleetReportAsync(CancellationToken cancellationToken)
        {
            var totalVehicles = await _context.Vehicles.CountAsync(cancellationToken);
            var rentedVehicles = await _context.Reservations.CountAsync(r => r.IsActive, cancellationToken);
            var availableVehicles = totalVehicles - rentedVehicles;

            var totalReservations = await _context.Reservations.CountAsync(cancellationToken);
            var activeReservations = await _context.Reservations.CountAsync(r => r.IsActive, cancellationToken);
            var canceledReservations = await _context.Reservations.CountAsync(r => !r.IsActive, cancellationToken);

            return new FleetReport
            {
                TotalVehicles = totalVehicles,
                AvailableVehicles = availableVehicles,
                RentedVehicles = rentedVehicles,
                TotalReservations = totalReservations,
                ActiveReservations = activeReservations,
                CanceledReservations = canceledReservations
            };
        }
    }
}