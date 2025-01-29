Damian Połubok

## Setup Instructions

### Configure `.env`
Create a `.env` file in the **solution directory**. **Example values**:
```ini
SA_PASSWORD=VehicleRentalPHOLDER@123
ASPNETCORE_ENVIRONMENT=Development
```

### Configure `appsettings.json`
Create `appsettings.json` file in the **project directory**. **Example values**: 
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost,1434;Database=VehicleRentalDb;User=sa;Password=;Encrypt=false;"
  },
  "Jwt": {
    "Key": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9@PLACEHOLDER",
    "Issuer": "VehicleRentalAPI",
    "Audience": "VehicleRentalAPI"
  }
}
```

#### Migrations and database updates run automatically when using docker-compose up. To perform dotnet ef commands on database, add password to the connection string in **appsettings.json**

### Run the Application with Docker
```sh
docker-compose up --build
```
### Access swagger at https://localhost:5001/swagger/index.html

### Use the same connection string from appsettings.json to connect to the database in SSMS.


# Port Troubleshooting

### If the application fails to connect to SQL Server, it may be due to port conflicts.

### I mapped it in docker-compose.yml like this:
```sh 
   ports:
      - "1434:1433"
```
If 1434 is already used then map it to 1435 and change connection string accordingly.
