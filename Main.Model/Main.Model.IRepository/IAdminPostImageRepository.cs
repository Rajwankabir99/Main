using Main.Common;
namespace Main.Model.Repository;

public interface IAdminPostImageRepository
{
    Task<List<AdminPost>> GetSelectAdminPosts(EnumCompanyName company);
}