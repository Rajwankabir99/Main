using Main.Infrastructure;

using Microsoft.AspNetCore.Authorization;

namespace WebAppCore.Helper;

public class TenantRoleHandler: AuthorizationHandler<TenantRoleRequirement>
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public TenantRoleHandler ( IHttpContextAccessor httpContextAccessor )
    {
        _httpContextAccessor = httpContextAccessor;
    }

    protected override Task HandleRequirementAsync ( AuthorizationHandlerContext context,TenantRoleRequirement requirement )
    {
        // 1. Bypass check entirely if user is a GlobalAdmin
        if ( context.User.IsInRole ( "GlobalAdmin" ) )
        {
            context.Succeed ( requirement );
            return Task.CompletedTask;
        }

        // 2. Safely extract the current Tenant ID from Route Data (e.g., /app/{tenantId}/products)
        var routeData = _httpContextAccessor.HttpContext?.GetRouteData();
        var currentTenantId = routeData?.Values["tenantId"]?.ToString();

        if ( string.IsNullOrEmpty ( currentTenantId ) )
        {
            return Task.CompletedTask; // No tenant context found in URL
        }

        // 3. Validate against the formatted "TenantId:RoleName" claim we made earlier
        var expectedClaimValue = $"{currentTenantId}:{requirement.AllowedRole}";

        if ( context.User.HasClaim ( c => c.Type == "TenantRole" && c.Value == expectedClaimValue ) )
        {
            context.Succeed ( requirement );
        }

        return Task.CompletedTask;
    }
}