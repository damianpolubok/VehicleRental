using VehicleRental.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.ConfigureIdentity(builder.Configuration);
builder.Services.AddSwaggerDocumentation();
builder.Services.AddControllers();

var app = builder.Build();
app.ApplyMigrations();
app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
