using Microsoft.EntityFrameworkCore;
using VehicleRental.API.Data;
using VehicleRental.API.Services.IServices;
using VehicleRental.API.Services;
using VehicleRental.API.Validation;
using VehicleRental.API.Repositories.Commands.ICommands;
using VehicleRental.API.Repositories.Commands;
using VehicleRental.API.Repositories.Queries.IQueries;
using VehicleRental.API.Repositories.Queries;

namespace VehicleRental.API.Extensions
{
    public static class ApplicationServicesExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IVehicleQueryRepository, VehicleQueryRepository>();
            services.AddScoped<IVehicleCommandRepository, VehicleCommandRepository>();
            services.AddScoped<IReservationQueryRepository, ReservationQueryRepository>();
            services.AddScoped<IReservationCommandRepository, ReservationCommandRepository>();
            services.AddScoped<IUserQueryRepository, UserQueryRepository>();
            services.AddScoped<IFleetQueryRepository, FleetQueryRepository>();

            services.AddScoped<IReservationPricingService, ReservationPricingService>();
            services.AddScoped<ITokenService, TokenService>();

            services.AddHostedService<ReservationCleanupService>();

            services.AddDbContext<VehicleRentalDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(typeof(Program).Assembly);
            });

            services.AddAutoMapper(typeof(Program));
            services.AddHttpContextAccessor();
            services.AddProblemDetails();
            services.AddExceptionHandler<ProblemDetailsExceptionHandler>();

            services.Configure<HostOptions>(options =>
            {
                options.BackgroundServiceExceptionBehavior = BackgroundServiceExceptionBehavior.Ignore;
            });

            return services;
        }
    }
}