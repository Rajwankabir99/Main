using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Main.Infrastructure;

public static class RegisterDatabases
{
    public static IServiceCollection AddDatabases ( 
        this IServiceCollection services,IConfiguration configuration )
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<ApplicationDbContext> ( options =>
        {
            options.UseSqlServer ( connectionString );
        } );

        services.AddDbContext<BussinessAppDbContext> ( options =>
        {
            options.UseLazyLoadingProxies ( );
            options.UseSqlServer ( connectionString );
        } );

        return services;
    }
}

