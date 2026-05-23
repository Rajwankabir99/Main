using DataTransferModel;

namespace Main.Services;

public interface IProductService
{
    Task<bool> SaveNewProduct(ProductDataModel objPostDm );

    Task<bool> UpdateProduct(ProductDataModel objPostDm );

    Task<bool> DeleteProductImage(int id, int productId);

    Task<bool> DeleteProduct(int productId);

    Task<ProductDataModel> GetProductForEditProductID ( int productID );

    Task<List<ProductDisplayModel>> GetAllProducts ( );
}

