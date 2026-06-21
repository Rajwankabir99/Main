using Main.Common.Enums;
using Main.Common.HelperServices;
using Main.Common.Model;
using Main.Infrastructure;

using Microsoft.AspNetCore.Mvc;
namespace Main.WebAppCore;

public class BottomHeaderPanelViewComponent: ViewComponent
{
    private EnumTenantStore EnumTenantStore
    {
        get; set;
    }

    public BottomHeaderPanelViewComponent ( ITenantSetter tenantSetter )
    {
        EnumTenantStore = tenantSetter.TenantStore;
    }

    public async Task<IViewComponentResult> InvokeAsync ( )
    {
        MenuObjectModel menuObjectModel = new MenuObjectModel
        {
            ListCategory = new List<TenantVariableModel> ( ),
            ListSubCategory = new List<TenantVariableModel> ( )
        };

        menuObjectModel.ListCategory = TenantStoreHelper.GetCategoryList ( EnumTenantStore );

        menuObjectModel.ListSubCategory = TenantStoreHelper.GetSubCategoryList ( EnumTenantStore );

        return View ( menuObjectModel );
    }
}
