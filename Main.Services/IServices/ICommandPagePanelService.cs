using DataTransferModel;
using DataTransferModel;

namespace Application.Service;

public interface ICommandPagePanelService
{
    Task<bool> CreateNewPanels (
        LocalModel model,
        EnumCompanyName enumCompany,
        List<PanelPostDataModel> listUserSelectedPosts,
        ModelBase modelBase
        );
}

