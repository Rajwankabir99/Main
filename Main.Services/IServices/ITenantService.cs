using DataTransferModel;

namespace Main.Services;

public interface ITenantService
{
    string TenantTd
    {
        get;
        set;
    }

    bool TenancyFound
    {
        get; set;
    }

    TenantDisplayDataModel? CurrentTenant
    {
        get; set;
    }

    Task FindTenant ( string? hostName );
}
