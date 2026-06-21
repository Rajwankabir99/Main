using Main.Common.HelperServices;
using Main.Common.Model;
using Main.Infrastructure;

using Microsoft.AspNetCore.Mvc;

namespace Main.WebAppCore;

public class CategoryMenuViewComponent: ViewComponent
{
    private readonly ITenantSetter _tenantSetter;

    public CategoryMenuViewComponent ( ITenantSetter tenantSetter )
    {
        _tenantSetter = tenantSetter;
    }

    public async Task<IViewComponentResult> InvokeAsync ( )
    {
        MenuObjectModel menuObjectModel = new MenuObjectModel
        {
            ListCategory = new List<TenantVariableModel> ( ),
            ListSubCategory = new List<TenantVariableModel> ( )
        };

        menuObjectModel.ListCategory = TenantStoreHelper.GetCategoryList ( _tenantSetter.TenantStore );
        menuObjectModel.ListSubCategory = TenantStoreHelper.GetSubCategoryList ( _tenantSetter.TenantStore );


        return View ( menuObjectModel );
    }
}
