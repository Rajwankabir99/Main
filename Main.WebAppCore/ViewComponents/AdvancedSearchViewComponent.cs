using Main.Infrastructure;

using Microsoft.AspNetCore.Mvc;

using WebAppCore.Helper;
namespace Main.WebAppCore;

public class AdvancedSearchViewComponent: ViewComponent
{
    private readonly ITenantSetter _tenantSetter;

    public AdvancedSearchViewComponent ( ITenantSetter tenantSetter )
    {
        _tenantSetter = tenantSetter;
    }

    public async Task<IViewComponentResult> InvokeAsync ( )
    {
        MenuObjectModel menuObjectModel = new MenuObjectModel();

        menuObjectModel.AV_Category = DropDownListItems.GetCategoryList ( _tenantSetter.TenantStore );

        menuObjectModel.AV_SubCategory = DropDownListItems.GetSubCategoryList ( _tenantSetter.TenantStore );

        return View ( menuObjectModel );
    }
}
