using Microsoft.EntityFrameworkCore;
using VehicleRental.API.Data;

namespace VehicleRental.API.Extensions
{
    public static class MigrationExtension
    {
        public static void ApplyMigrations(this IApplicationBuilder app)
        {
            using IServiceScope scope = app.ApplicationServices.CreateScope();

            using VehicleRentalDbContext dbContext =
                scope.ServiceProvider.GetRequiredService<VehicleRentalDbContext>();

            if (dbContext.Database.IsRelational())
            {
                if (!dbContext.Database.GetAppliedMigrations().Any())
                {
                    dbContext.Database.EnsureCreated();
                }
                else
                {
                    dbContext.Database.Migrate();
                }
            }
            else
            {
                dbContext.Database.EnsureCreated();
            }
        }
    }
}