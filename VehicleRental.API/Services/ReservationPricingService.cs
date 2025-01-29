using System.ComponentModel.DataAnnotations;
using VehicleRental.API.Services.IServices;

namespace VehicleRental.API.Services
{
    public class ReservationPricingService : IReservationPricingService
    {
        public decimal CalculateTotalPrice(DateTime startDate, DateTime endDate, decimal rentalPricePerMinute)
        {
            if (startDate >= endDate)
            {
                throw new ValidationException("Start date must be earlier than end date.");
            }

            var totalMinutes = (decimal)(endDate - startDate).TotalMinutes;
            return Math.Round(totalMinutes * rentalPricePerMinute, 2);
        }
    }
}