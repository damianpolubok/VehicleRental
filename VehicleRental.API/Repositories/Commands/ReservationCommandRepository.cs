using VehicleRental.API.Data;
using VehicleRental.API.Models.Reservations;
using VehicleRental.API.Repositories.Commands.ICommands;

namespace VehicleRental.API.Repositories.Commands
{
    public class ReservationCommandRepository : IReservationCommandRepository
    {
        private readonly VehicleRentalDbContext _context;

        public ReservationCommandRepository(VehicleRentalDbContext context)
        {
            _context = context;
        }

        public async Task AddReservationAsync(Reservation reservation, CancellationToken cancellationToken)
        {
            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateReservationAsync(Reservation reservation, CancellationToken cancellationToken)
        {
            var existingReservation = await _context.Reservations.FindAsync(new object[] { reservation.Id }, cancellationToken);
            if (existingReservation == null)
            {
                throw new KeyNotFoundException($"Reservation with ID {reservation.Id} was not found.");
            }

            existingReservation.IsActive = reservation.IsActive;
            existingReservation.StartDate = reservation.StartDate;
            existingReservation.EndDate = reservation.EndDate;
            existingReservation.TotalPrice = reservation.TotalPrice;

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}