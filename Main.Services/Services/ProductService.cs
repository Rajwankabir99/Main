using DataTransferModel;

using Domain.Model;

using IRepository;

using Main.Services.Extensions;

namespace Main.Services.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _ProductRepository;

    public ProductService ( IProductRepository productRepository)
    {
        _ProductRepository = productRepository;
    }

    public async Task<List<ProductDisplayModel>> GetAllProducts()
    {
        var listProducts = await _ProductRepository.GetAllProducts();

        List<ProductDisplayModel> objListPostDisplayModel
              = ProductServiceMapping.GetProductDisplayModels(listProducts);

        return objListPostDisplayModel;
    }


    public async Task<bool> SaveNewProduct(ProductDataModel objProductDM)
    {
        return await _ProductRepository
                        .SaveNewProduct(objProductDM, objProductDM.ImageFiles);
    }

    public async Task<ProductDataModel> GetProductForEditProductID(int productID)
    {
        return await _ProductRepository
            .GetProductByProductID(productID);
    }

    public async Task<bool> UpdateProduct(ProductDataModel  objProductVm)
    {
        return await _ProductRepository
                    .UpdateProduct ( objProductVm );
    }

    public async Task<bool> DeleteProductImage(int id, int postId)
    {
        return await _ProductRepository
                    .DeleteProductImage (id, postId);
    }

    public async Task<bool> DeleteProduct(int postId)
    {
        return await _ProductRepository.DeleteProduct(postId);
    }
}

