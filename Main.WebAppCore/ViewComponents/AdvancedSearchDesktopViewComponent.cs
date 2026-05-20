using Microsoft.AspNetCore.Mvc;
using Main.Common.Settings;
using Main.Common.Enums;

namespace FineArtsWebApp
{
    public class AdvancedSearchDesktopViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            MenuObjectModel menuObjectModel = 
                new MenuObjectModel(
                    (EnumCategoryFor)AppSettings.Current.EnumCategoryFor);

            return View(menuObjectModel);
        }
    }
}
