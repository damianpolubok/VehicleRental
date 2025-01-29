using VehicleRental.API.Models.Reservations;

namespace VehicleRental.API.Repositories.Queries.IQueries
{
    public interface IReservationQueryRepository
    {
        Task<IEnumerable<Reservation>> GetOverdueReservationsAsync(DateTime currentDate, CancellationToken cancellationToken);
        Task<Reservation?> GetReservationByIdAsync(int reservationId, CancellationToken cancellationToken);
        Task<IEnumerable<Reservation>> GetReservationsByUserIdAsync(string userId, CancellationToken cancellationToken);
        Task<bool> IsVehicleReservedAsync(int vehicleId, DateTime startDate, DateTime endDate, int? excludeReservationId, CancellationToken cancellationToken);
    }
}