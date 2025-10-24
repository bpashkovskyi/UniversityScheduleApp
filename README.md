# University Schedule App

A .NET 9 solution for managing university schedule data with separate projects for data access, console application, and web application.

## Project Structure

```
UniversityScheduleApp/
├── UniversityScheduleApp.sln              # Solution file
├── UniversityScheduleApp.Data/            # Data layer (Class Library)
│   ├── Models/                           # Entity models
│   ├── Services/                         # API services
│   └── Responses/                        # API response models
├── UniversityScheduleApp.Console/         # Console application
│   └── Program.cs                        # Data fetcher console app
└── UniversityScheduleApp.Web/             # Web application
    ├── Controllers/                      # MVC controllers
    ├── Views/                           # Razor views
    ├── Properties/                      # Launch settings
    └── appsettings.json                 # Configuration
```

## Projects

### UniversityScheduleApp.Data
- **Type**: Class Library
- **Purpose**: Contains all shared models, services, and API response classes
- **Dependencies**: Entity Framework Core, System.Text.Json

### UniversityScheduleApp.Console
- **Type**: Console Application
- **Purpose**: Data fetcher that creates database and populates it with API data
- **Features**:
  - Creates database if not exists
  - Fetches rooms, teachers, and groups from API
  - Shows progress and summary statistics
  - Error handling

### UniversityScheduleApp.Web
- **Type**: ASP.NET Core Web Application
- **Purpose**: Web interface for viewing schedule data
- **Features**:
  - MVC controllers and views
  - Database integration
  - LocalDB connection

## Database

- **Provider**: SQL Server LocalDB
- **Connection String**: `Server=(localdb)\\mssqllocaldb;Database=UniversityScheduleDb;Trusted_Connection=true;MultipleActiveResultSets=true`
- **Auto-creation**: Database is created automatically when running the console app

## Running the Applications

### Console App (Data Fetcher)
```bash
dotnet run --project UniversityScheduleApp.Console
```

### Web App
```bash
dotnet run --project UniversityScheduleApp.Web
```

### Build All
```bash
dotnet build
```

## Dependencies

- .NET 9.0
- Entity Framework Core 9.0.10
- Microsoft.Extensions.Hosting
- Microsoft.Extensions.DependencyInjection
- System.Text.Json