using DataTransferModel;

using Main.Common.Enums;

using WebAppCore.Helper;

namespace WebAppCore.ViewModel.Extensions;

public static class PageMapping
{
    public static List<PageDisplayViewModel> PageDisplayMapping ( List<PageDisplayDataModel> listPageDataModel )
    {
        List<PageDisplayViewModel> listPageDisplayViewModels = new List<PageDisplayViewModel> ();

        PageDisplayViewModel pageDisplayViewModel;

        listPageDataModel.ForEach ( dataModel =>
        {
            pageDisplayViewModel = new PageDisplayViewModel ( );

            pageDisplayViewModel.PageName = ListEnum.GetPageDescription ( dataModel.EnumPublicPage );

            pageDisplayViewModel.CompanyName =
                                            ListEnum.GetCompanyDescription ( dataModel.EnumCompanyName );

            listPageDisplayViewModels.Add ( pageDisplayViewModel );
        } );

        return listPageDisplayViewModels;
    }

    public static List<PanelPostSelectViewModel> MapSelectPostViewModel ( List<PanelPostDataModel> listSelectProductsDataModels,EnumCategoryFor categoryFor,EnumCurrency currency )
    {
        if ( listSelectProductsDataModels == null )
        {
            return new List<PanelPostSelectViewModel> ( );
        }

        List<PanelPostSelectViewModel> listPanelPostSelectViewModels =
            new List<PanelPostSelectViewModel> ();

        PanelPostSelectViewModel panelPostSelectViewModel;

        listSelectProductsDataModels.ForEach ( dataModel =>
        {
            panelPostSelectViewModel = new PanelPostSelectViewModel ( dataModel.EnumPostType,
                dataModel.RootID,dataModel.ImageFileID,dataModel.ImageOrderID );

            panelPostSelectViewModel.ImageFileContent = dataModel.ImageFileContent;
            panelPostSelectViewModel.CategoryName = SelectListItemDropDown.GetCategoryText ( categoryFor,
                dataModel.CategoryID );

            panelPostSelectViewModel.PostTitle = dataModel.PostTitle;
            panelPostSelectViewModel.Price = dataModel.Price;
            panelPostSelectViewModel.Currency = ListEnum.GetCurrencyDescription ( currency );
            panelPostSelectViewModel.PanelPostID = dataModel.PanelPostID;

            listPanelPostSelectViewModels.Add ( panelPostSelectViewModel );

        } );

        return listPanelPostSelectViewModels;
    }
}