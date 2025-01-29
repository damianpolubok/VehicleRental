using VehicleRental.API.Repositories.Commands.ICommands;
using VehicleRental.API.Repositories.Queries.IQueries;

namespace VehicleRental.API.Services
{
    public class ReservationCleanupService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<ReservationCleanupService> _logger;
        private readonly TimeSpan _checkInterval = TimeSpan.FromMinutes(1);

        public ReservationCleanupService(IServiceProvider serviceProvider, ILogger<ReservationCleanupService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using var scope = _serviceProvider.CreateScope();
                    var reservationQueryRepository = scope.ServiceProvider.GetRequiredService<IReservationQueryRepository>();
                    var reservationCommandRepository = scope.ServiceProvider.GetRequiredService<IReservationCommandRepository>();

                    var now = DateTime.UtcNow;
                    var overdueReservations = await reservationQueryRepository.GetOverdueReservationsAsync(now, stoppingToken);

                    _logger.LogInformation($"Found {overdueReservations.Count()} overdue reservations.");

                    foreach (var reservation in overdueReservations)
                    {
                        try
                        {
                            _logger.LogInformation($"Marking reservation {reservation.Id} as inactive.");
                            reservation.IsActive = false;
                            await reservationCommandRepository.UpdateReservationAsync(reservation, stoppingToken);
                            _logger.LogInformation($"Reservation {reservation.Id} successfully updated.");
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex, $"Error updating reservation {reservation.Id}. Skipping to next.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Unexpected error during reservation cleanup. Retrying in next cycle.");
                }

                await Task.Delay(_checkInterval, stoppingToken);
            }
        }
    }
}