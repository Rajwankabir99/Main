using Main.Common;
using Main.Model;

namespace Data
{
    public interface IAdminPostImageRepository
    {
        Task<List<AdminPost>> GetSelectAdminPosts(EnumCompanyName company);

    }
}
