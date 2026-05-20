using Main.Common.Enums;
using Main.Common.Settings;
using Microsoft.AspNetCore.Mvc;

namespace FineArtsWebApp.ViewComponents
{
    public class MainHeaderPanelViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            MenuObjectModel menuObjectModel 
                = new MenuObjectModel((EnumCategoryFor)AppSettings.Current.EnumCategoryFor);

            return View(menuObjectModel);
        }
    }
}
