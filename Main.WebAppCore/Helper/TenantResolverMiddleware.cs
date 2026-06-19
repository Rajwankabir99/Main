using Main.Infrastructure;
using Main.Services;
namespace WebAppCore.Helper;

public class TenantResolverMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ITenantService _tenantService;

    public TenantResolverMiddleware ( RequestDelegate next,ITenantService tenantService )
    {
        _next = next;
        _tenantService = tenantService;
    }

    public async Task InvokeAsync ( HttpContext context,ITenantSetter tenantSetter )
    {
        string host = context.Request.Host.Host ?? string.Empty;

        // Localhost for fine arts (development)
        string tenantId = "e02fd0e1-00fd-009a-ca30-0d00a2345ba0";

        if ( !string.IsNullOrWhiteSpace ( host ) && host != "localhost" )
        {
            string[] segments = host.Split('.');

            if ( segments.Length > 0 && segments[0] == "www" )
            {
                segments = segments.Skip ( 1 ).ToArray ( );
                host = string.Join ( ".",segments );
            }

            if ( segments.Length > 2 )
            {
                string subdomain = segments[0];
                await _tenantService.FindTenant ( subdomain );
            }

            if ( !_tenantService.TenancyFound )
            {
                string subdomain = segments[0];
                await _tenantService.FindTenant ( subdomain );
            }
            else
            {
                tenantId = _tenantService.TenantTd;
            }
        }

        // development
        if ( host == "localhost" )
        {
        }

        tenantSetter.CurrentTenantId = tenantId;

        await _next ( context );
    }
}