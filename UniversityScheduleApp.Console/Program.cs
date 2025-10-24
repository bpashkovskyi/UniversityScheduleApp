using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using UniversityScheduleApp.Data.Models;
using UniversityScheduleApp.Data.Services;
using SystemConsole = System.Console;

namespace UniversityScheduleApp.Console;

class Program
{
    static async Task Main(string[] args)
    {
        SystemConsole.WriteLine("University Schedule App - Data Fetcher");
        SystemConsole.WriteLine("=====================================");

        // Create host builder
        var host = Host.CreateDefaultBuilder(args)
            .ConfigureServices((context, services) =>
            {
                // Add DbContext with LocalDB connection string
                services.AddDbContext<UniversityContext>(options =>
                    options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=UniversityScheduleDb;Trusted_Connection=true;MultipleActiveResultSets=true"));

                // Add HttpClient
                services.AddHttpClient<ApiService>();

                // Add ApiService
                services.AddScoped<ApiService>();
            })
            .Build();

        try
        {
            using var scope = host.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<UniversityContext>();
            var apiService = scope.ServiceProvider.GetRequiredService<ApiService>();

            // Ensure database is created
            SystemConsole.WriteLine("Creating database if not exists...");
            await context.Database.EnsureCreatedAsync();
            SystemConsole.WriteLine("✓ Database ready");

            // Fetch and save rooms
            SystemConsole.WriteLine("\nFetching rooms...");
            await apiService.FetchAndSaveRoomsAsync();
            SystemConsole.WriteLine("✓ Rooms fetched and saved");

            // Fetch and save teachers
            SystemConsole.WriteLine("\nFetching teachers...");
            await apiService.FetchAndSaveTeachersAsync();
            SystemConsole.WriteLine("✓ Teachers fetched and saved");

            // Fetch and save groups
            SystemConsole.WriteLine("\nFetching groups...");
            await apiService.FetchAndSaveGroupsAsync();
            SystemConsole.WriteLine("✓ Groups fetched and saved");

            // Display summary
            SystemConsole.WriteLine("\n=== Summary ===");
            var roomCount = await context.Rooms.CountAsync();
            var teacherCount = await context.Teachers.CountAsync();
            var groupCount = await context.Groups.CountAsync();
            var blockCount = await context.Blocks.CountAsync();
            var groupDeptCount = await context.GroupDepartments.CountAsync();
            var teacherDeptCount = await context.TeacherDepartments.CountAsync();

            SystemConsole.WriteLine($"Blocks: {blockCount}");
            SystemConsole.WriteLine($"Rooms: {roomCount}");
            SystemConsole.WriteLine($"Group Departments: {groupDeptCount}");
            SystemConsole.WriteLine($"Groups: {groupCount}");
            SystemConsole.WriteLine($"Teacher Departments: {teacherDeptCount}");
            SystemConsole.WriteLine($"Teachers: {teacherCount}");

            SystemConsole.WriteLine("\n✓ All data fetched successfully!");
        }
        catch (Exception ex)
        {
            SystemConsole.WriteLine($"\n❌ Error: {ex.Message}");
            SystemConsole.WriteLine($"Stack trace: {ex.StackTrace}");
        }

        SystemConsole.WriteLine("\nPress any key to exit...");
        SystemConsole.ReadKey();
    }
}