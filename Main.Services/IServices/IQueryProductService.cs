using DataTransferModel;

namespace Application.Service;

public interface IQueryProductService
{
    Task<List<ProductDataModel>> GetAllProducts();

    Task<ProductDataModel> GetProductForEditProductID(int productID);
}

