using Microsoft.EntityFrameworkCore;
using VehicleRental.API.Data;
using VehicleRental.API.Models.Reservations;
using VehicleRental.API.Repositories.Queries.IQueries;

namespace VehicleRental.API.Repositories.Queries
{
    public class ReservationQueryRepository : IReservationQueryRepository
    {
        private readonly VehicleRentalDbContext _context;

        public ReservationQueryRepository(VehicleRentalDbContext context)
        {
            _context = context;
        }

        public async Task<bool> IsVehicleReservedAsync(int vehicleId, DateTime startDate, DateTime endDate, int? excludeReservationId, CancellationToken cancellationToken)
        {
            return await _context.Reservations.AnyAsync(r =>
                r.VehicleId == vehicleId &&
                r.IsActive &&
                (excludeReservationId == null || r.Id != excludeReservationId) &&
                r.StartDate < endDate &&
                r.EndDate > startDate, cancellationToken);
        }

        public async Task<Reservation?> GetReservationByIdAsync(int reservationId, CancellationToken cancellationToken)
        {
            return await _context.Reservations
                .AsNoTracking()
                .Include(r => r.Vehicle)
                .FirstOrDefaultAsync(r => r.Id == reservationId, cancellationToken);
        }

        public async Task<IEnumerable<Reservation>> GetOverdueReservationsAsync(DateTime currentDate, CancellationToken cancellationToken)
        {
            return await _context.Reservations.AsNoTracking()
                .Where(r => r.IsActive && r.EndDate <= currentDate)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Reservation>> GetReservationsByUserIdAsync(string userId, CancellationToken cancellationToken)
        {
            return await _context.Reservations
                .AsNoTracking()
                .Include(r => r.Vehicle)
                .Where(r => r.UserId == userId)
                .ToListAsync(cancellationToken);
        }
    }
}