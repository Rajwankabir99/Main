
using Main.Common;
using Main.Model;

namespace Data
{
    public interface IProductImageRepository
    {
        Task<List<Product>> GetSelectProducts(EnumCompanyName company);

    }
}
