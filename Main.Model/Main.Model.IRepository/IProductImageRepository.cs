
using Main.Common;
namespace Main.Model.Repository;

public interface IProductImageRepository
{
    Task<List<Product>> GetSelectProducts(EnumCompanyName company);
}
