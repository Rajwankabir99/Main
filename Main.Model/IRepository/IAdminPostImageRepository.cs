using BusinessModel;
using Main.Common.Enums;

namespace IRepository;

public interface IAdminPostImageRepository
{
    Task<List<PanelPostDataModel>> GetSelectAdminPosts ( EnumCompanyName company );
}