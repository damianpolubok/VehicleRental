using VehicleRental.API.Models.Reservations;

namespace VehicleRental.API.Repositories.Commands.ICommands
{
    public interface IReservationCommandRepository
    {
        Task AddReservationAsync(Reservation reservation, CancellationToken cancellationToken);
        Task UpdateReservationAsync(Reservation reservation, CancellationToken cancellationToken);
    }
}