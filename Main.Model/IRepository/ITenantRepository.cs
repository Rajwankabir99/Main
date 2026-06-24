using Domain.Model;

namespace Main.IRepository;

public interface ITenantRepository
{
    TenantInfo? CurrentTenant
    {
        get; set;
    }

    Task FindCurrentTenantAsync ( string? hostName );

}
