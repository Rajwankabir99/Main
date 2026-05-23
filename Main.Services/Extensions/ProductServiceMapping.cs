
using DataTransferModel;

using Domain.Model;

namespace Main.Services.Extensions;

public static class ProductServiceMapping
{
    public static List<ProductDisplayModel> GetProductDisplayModels ( List<Product> listProducts )
    {
        List<ProductDisplayModel> objListPostDisplayModel
            = new List<ProductDisplayModel>();

        ProductDisplayModel objProductDisplayModel;

        foreach ( Product item in listProducts.ToList ( ) )
        {
            objProductDisplayModel = new ProductDisplayModel ( );

            MapProductDisplayModel ( item,objProductDisplayModel );

            objListPostDisplayModel.Add ( objProductDisplayModel );
        }

        return objListPostDisplayModel;
    }

    private static void MapProductDisplayModel (
        Product productEntity,ProductDisplayModel productDisplayModel )
    {
        productDisplayModel.ProductID = productEntity.ProductID;
        productDisplayModel.CategoryID = productEntity.CategoryID;
        productDisplayModel.SubCategoryID = productEntity.SubCategoryID;
        productDisplayModel.ProductName = productEntity.ProductName;
        productDisplayModel.UnitPrice = productEntity.Price;
        productDisplayModel.Discount = productEntity.Discount;
    }
}
