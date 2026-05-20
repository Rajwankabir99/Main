using BusinessModel;
using Main.Common.Enums;
using Main.Common.Model;

namespace IService;

public interface IPagePanelDataService
{
    Task<List<PanelPostDataModel>>
        GetSelectProducts ( EnumCompanyName company );

    Task<bool> CreateNewPanels (
        LocalModel model,
        EnumCompanyName enumCompany,
        List<PanelPostDataModel> listUserSelectedPosts,
        ModelBase modelBase
        );

    //Task<List<PanelPostDataModel>> GetSelectAdminPosts              (EnumCompanyName company);

    //Task<PagePanelDataModel> GetPreviewPanel ( int panelId );

    Task<PageDataModel> GetPanelList ( int pageID );
}

