using DataTransferModel;
using Main.Common.Enums;

namespace Application.Service;

public interface IQueryPagePanelService
{
    Task<List<PanelPostDataModel>>
        GetSelectProducts ( EnumCompanyName company );

    Task<PageDataModel> GetPanelList ( int pageID );

}

