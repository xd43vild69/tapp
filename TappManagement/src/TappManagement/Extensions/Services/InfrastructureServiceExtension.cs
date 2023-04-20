namespace TappManagement.Extensions.Services;

using TappManagement.Databases;
using TappManagement.Resources;
using TappManagement.Services;
using Configurations;
using Microsoft.EntityFrameworkCore;

public static class ServiceRegistration
{
    public static void AddInfrastructure(this IServiceCollection services, IWebHostEnvironment env, IConfiguration configuration)
    {
        // DbContext -- Do Not Delete
        var connectionString = configuration.GetConnectionStringOptions().TappManagement;
        if(string.IsNullOrWhiteSpace(connectionString))
        {
            // this makes local migrations easier to manage. feel free to refactor if desired.
            connectionString = env.IsDevelopment() 
                ? "Server=sqlservertest13.database.windows.net,1433;Database=TappDB;User Id=;Password=;"
                : throw new Exception("The database connection string is not set.");
        }

        services.AddDbContext<TappDbContext>(options =>
            options.UseSqlServer(connectionString,
                builder => builder.MigrationsAssembly(typeof(TappDbContext).Assembly.FullName)));

        services.AddHostedService<MigrationHostedService<TappDbContext>>();

        // Auth -- Do Not Delete
    }
}
