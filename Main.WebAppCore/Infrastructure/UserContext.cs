using Main.Services;
using System.Security.Claims;

namespace WebApp.Infrastructure;

public class UserContext: IUserContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IConfiguration _configuration;

    public UserContext ( IHttpContextAccessor httpContextAccessor,IConfiguration configuration )
    {
        _httpContextAccessor = httpContextAccessor;
        _configuration = configuration;
    }
    
    private ClaimsPrincipal? User => _httpContextAccessor.HttpContext?.User;

    
    public string UserId => User?.FindFirst ( ClaimTypes.NameIdentifier )?.Value ?? "System";

    public string IdentityId => User?.FindFirst ( "IdentityId" )?.Value ?? string.Empty;

    public string Company => User?.FindFirst ( "Company" )?.Value ?? string.Empty;

    public string Currency => User?.FindFirst ( "Currency" )?.Value ?? string.Empty;

    public string Country => User?.FindFirst ( "Country" )?.Value ?? string.Empty;

    public string AppEnvironment => _configuration["EnvironmentSettings:Name"] ?? "Production";

    public DateTime GetLocalNow ( )
    {
        string timeZoneId = Country switch
        {
            "BD" => "Bangladesh Standard Time",
            "US" => "Eastern Standard Time",
            _ => "UTC" 
        };

        TimeZoneInfo userTimeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
        return TimeZoneInfo.ConvertTimeFromUtc ( DateTime.UtcNow,userTimeZone );
    }
}
