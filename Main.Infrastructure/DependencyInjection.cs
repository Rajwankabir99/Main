// File: MyApp.Infrastructure/DependencyInjection.cs
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Main.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices (
        this IServiceCollection services,
        IConfiguration configuration )
    {
        // EF Core registration happens safely inside this library
        services.AddDbContext<WebBusinessEntityContext> ( options =>
            options.UseSqlServer (
                configuration.GetConnectionString ( "DefaultConnection" ),
                b => b.MigrationsAssembly ( typeof ( WebBusinessEntityContext ).Assembly.FullName ) ) );

        // Register your implementations against domain interfaces
        //services.AddScoped<IUserRepository,UserRepository> ( );

        return services;
    }
}

