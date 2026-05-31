using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Main.Infrastructure;

public static class RegisterIdentitySecurity
{
    public static IServiceCollection AddIdentitySecurity (
        this IServiceCollection services,IConfiguration configuration )
    {

        var identitySettings = configuration.GetSection("IdentitySettings");
        var password = identitySettings.GetSection("Password");
        var lockOut = identitySettings.GetSection("Lockout");
        var signIn = identitySettings.GetSection("SignIn");

        services.AddIdentity<IdentityUser,IdentityRole> ( options =>
        {
            options.SignIn.RequireConfirmedEmail = signIn.GetValue<bool> ( "RequireConfirmedEmail" );
            options.Password.RequireDigit = password.GetValue<bool> ( "RequireDigit" );
            options.Password.RequireLowercase = password.GetValue<bool> ( "RequireLowercase" );
            options.Password.RequireUppercase = password.GetValue<bool> ( "RequireUppercase" );
            options.Password.RequireNonAlphanumeric = password.GetValue<bool>
                                                               ( "RequireNonAlphanumeric" );
            options.Password.RequiredLength = password.GetValue<int> ( "RequiredLength" );
            options.Lockout.DefaultLockoutTimeSpan = lockOut.GetValue<TimeSpan> ( "DefaultLockoutTimeSpan" );
            options.Lockout.MaxFailedAccessAttempts = lockOut.GetValue<int> ( "MaxFailedAccessAttempts" );
            options.Lockout.AllowedForNewUsers = lockOut.GetValue<bool> ( "AllowedForNewUsers" );
            options.User.RequireUniqueEmail = signIn.GetValue<bool> ( "RequireUniqueEmail" );
        } )
        .AddEntityFrameworkStores<ApplicationDbContext> ( )
        .AddDefaultTokenProviders ( )
        .AddSignInManager ( );

        var authenticationSettings = configuration.GetSection("Authentication");

        // Identity Application Cookie
        services.ConfigureApplicationCookie ( options =>
        {
            options.LoginPath = authenticationSettings.GetValue<string> ( "LoginPath" ) ?? "/Auth/Login";
            options.AccessDeniedPath = authenticationSettings.GetValue<string> ( "AccessDeniedPath" ) ?? "/Auth/AccessDenied";
            options.Cookie.Name = "YourApp_AuthCookie";
            options.Cookie.HttpOnly = authenticationSettings.GetValue<bool> ( "HttpOnly" );
            options.ExpireTimeSpan = TimeSpan.FromMinutes ( authenticationSettings.GetValue<int> ( "ExpireTimeSpanInMinutes" ) );
            options.SlidingExpiration = authenticationSettings.GetValue<bool> ( "SlidingExpiration" ); 
        } );

        services.AddControllersWithViews ( );
        
        services.AddAuthorization ( options =>
        {
            options.AddPolicy ( "RequireAdminRole",policy => policy.RequireRole ( "Admin" ) );
        } );

        services.AddMemoryCache ( options =>
        {
            options.SizeLimit = 1024;
            options.CompactionPercentage = 0.25;
        } );

        services.AddSession ( options =>
        {
            options.IdleTimeout = TimeSpan.FromMinutes ( 20 );
            options.Cookie.HttpOnly = true;
            options.Cookie.IsEssential = true;
        } );

        return services;
    }
}

