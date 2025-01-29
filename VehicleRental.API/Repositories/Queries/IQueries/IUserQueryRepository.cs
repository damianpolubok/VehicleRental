namespace VehicleRental.API.Repositories.Queries.IQueries
{
    public interface IUserQueryRepository
    {
        Task<bool> UserExistsAsync(string userId, CancellationToken cancellationToken);
    }
}