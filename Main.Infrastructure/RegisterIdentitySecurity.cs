using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

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
            //SignIn
            options.SignIn.RequireConfirmedEmail = signIn.GetValue<bool> ( "RequireConfirmedEmail" );

            //Password
            options.Password.RequireDigit = password.GetValue<bool> ( "RequireDigit" );
            
            options.Password.RequireLowercase = password.GetValue<bool> ( "RequireLowercase" );
            
            options.Password.RequireUppercase = password.GetValue<bool> ( "RequireUppercase" );
            
            options.Password.RequireNonAlphanumeric = password.GetValue<bool> ( "RequireNonAlphanumeric" );
            
            options.Password.RequiredLength = password.GetValue<int> ( "RequiredLength" );

            // Lockout settings
            options.Lockout.DefaultLockoutTimeSpan = lockOut.GetValue<TimeSpan> ( "DefaultLockoutTimeSpan" ); 

            options.Lockout.MaxFailedAccessAttempts = lockOut.GetValue<int> ( "MaxFailedAccessAttempts" );

            options.Lockout.AllowedForNewUsers = lockOut.GetValue<bool> ( "AllowedForNewUsers" );

            //User
            options.User.RequireUniqueEmail = signIn.GetValue<bool> ( "RequireUniqueEmail" );

        } )
        .AddEntityFrameworkStores<ApplicationDbContext> ( )
        .AddDefaultTokenProviders( )
        .AddSignInManager ( );


        //Authentication with JWT
        var jwtSettings = configuration.GetSection("JwtSettings");

        var secretKey = jwtSettings.GetValue<string>("Secret");


        services.AddAuthentication ( options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        } )
        .AddJwtBearer ( options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings.GetValue<string> ( "ValidIssuer" ),
                ValidAudience = jwtSettings.GetValue<string> ( "ValidAudience" ),
                IssuerSigningKey = new SymmetricSecurityKey ( Encoding.UTF8.GetBytes ( secretKey ?? "YourSuperSecretAndExtremelyLongKeyHere1234567890" ) ),
                RoleClaimType = ClaimTypes.Role,
                NameClaimType = ClaimTypes.NameIdentifier
            };

            options.RequireHttpsMetadata = false;
        } );

        services.AddAuthorization ( );


        var corsSettings = configuration.GetSection("CorsSettings");

        var allowedOrigins = corsSettings.GetSection("AllowedOrigins").Get<string[]> ( ) ?? Array.Empty<string> ( );    


        services.AddCors ( options =>
        {
            options.AddPolicy ( "AllowFrontendApp", policy =>
            {
                policy.WithOrigins ( allowedOrigins )
                      .AllowAnyMethod ( )                                       
                      .AllowAnyHeader ( )                                       
                      .AllowCredentials ( );                                    
            } );

            options.AddPolicy ( "AllowAll", policy =>
            {
                policy.AllowAnyOrigin ( )
                      .AllowAnyMethod ( )
                      .AllowAnyHeader ( );
            } );

        } );

        return services;

    }
}

