namespace VehicleRental.API.Services.IServices
{
    public interface IReservationPricingService
    {
        decimal CalculateTotalPrice(DateTime startDate, DateTime endDate, decimal rentalPricePerMinute);
    }
}