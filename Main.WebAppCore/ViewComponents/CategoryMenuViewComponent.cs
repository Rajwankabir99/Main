using Main.Common.HelperServices;
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
            ListCategory = TenantStoreHelper.GetCategoryList ( _tenantSetter.TenantStore ),

            ListSubCategory = TenantStoreHelper.GetSubCategoryList ( _tenantSetter.TenantStore )
        };

        return View ( menuObjectModel );
    }
}
