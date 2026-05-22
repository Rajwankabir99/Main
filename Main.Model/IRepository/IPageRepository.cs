using Domain.Model;
using Main.Common.Enums;
using Main.Common.Model;

namespace IRepository;

public interface IPageRepository            
{
    Task<List<Page>> GetAllPages(EnumCompanyName company);

    Task<Page> GetSinglePage(int id);

    Task<bool> PageExists(int id);

    Task<bool> CreateNewContent
    (
        LocalModel model,
        EnumCompanyName enumCompany,
        List<PanelPost> listUserSelectedPosts,
        ModelBase modelBase
    );
}
