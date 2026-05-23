using Data;

using Domain.Model;

using IRepository;
using Main.Common.Enums;
using Microsoft.EntityFrameworkCore;

using System.Xml.Linq;

namespace Repository;

public class ProductRepository : IProductRepository
{
    private readonly BussinessAppDbContext _Context;

    public ProductRepository( BussinessAppDbContext context )
    {
        _Context = context;
    }

    public async Task<bool> SaveChanges()
    {
        var result = await _Context.SaveChangesAsync();
        return result > 0;
    }

    public async Task<List<Product>> GetAllProducts()
    {
        return await _Context.Products.ToListAsync();
    }

    public async Task<bool> DeleteProduct(int productId)
    {
        var product = _Context
            .Products
            .ToList()
            .Single<Product>(a => a.ProductID == productId);

        if (product != null)
        {
            _Context.Products.Remove(product);
        }

        var result = await _Context.SaveChangesAsync();

        return result > 0;
    }

    public async Task<bool> DeleteProductImage(int id, int productId)
    {
        var image = await 
            _Context
            .ProductImageFiles
            .Where(a => 
                a.ProductImageFileID == id && 
                a.ProductID == productId)
            .FirstOrDefaultAsync();

        if (image != null)
        {
            _Context
                .ProductImageFiles
                .Remove(image);
        }

        var result = await _Context.SaveChangesAsync();

        return result > 0;
    }

    public async Task<Product> GetProductByProductID(int postId)
    {
        var productEntity = await _Context
            .Products
            .SingleAsync<Product>(a => a.ProductID == postId);

        List<ProductImageFile> objListFiles = new List<ProductImageFile>();

        if ( productEntity.ListImageFiles != null && productEntity.ListImageFiles.Count > 0 )
        {
            productEntity.ListImageFiles.ToList ( ).ForEach ( fileEntity =>
            {
                ProductFileDataModel objFileDM = new ProductFileDataModel()
                {
                    ProductImageFileID = fileEntity.ProductImageFileID,
                    ImageFileContent = fileEntity.ImageFileContent,
                    ProductID = fileEntity.ProductID
                };
                objListFiles.Add ( objFileDM );
            } );
        }


        List<ProductComment> objListComments = new List<ProductComment>();

        if ( productEntity.ListComments != null && productEntity.ListComments.Count > 0 )
        {
            productEntity.ListComments.ToList ( ).ForEach ( commentEntity =>
            {
                ProductCommentDataModel objCommentDM = new ProductCommentDataModel()
                {
                    ProductCommentID = commentEntity.ProductCommentID,
                    Comment = commentEntity.Comment,
                    ProductID = commentEntity.ProductID
                };
                objListComments.Add ( objCommentDM );
            } );
        }

        Product objModel = new Product()
        {
            ProductID = productEntity.ProductID,
            ProductName = productEntity.ProductName,
            Discount = productEntity.Discount,
            SaleCommission = productEntity.SaleCommission,
            SearchTag = productEntity.SearchTag,
            PostType = (EnumPostType)productEntity.PostType,
            Description = productEntity.Description,
            CategoryID = productEntity.CategoryID,
            SubCategoryID = productEntity.SubCategoryID,
            //UnitPrice = productEntity.Price,
            UserID = productEntity.UserID,
            ListComments = objListComments//,
            //ImageFiles = objListFiles
        };

        return objModel;
    }

    public async Task<bool> SaveNewProduct(Product objPostDM, List<ProductImageFile> objListFiles)
    {
        Product objProductEntity = MapProductViweModelToProductEntity(objPostDM);

        //objProductEntity.CreateBaseData ( objPostDM.ModelBase );

        objProductEntity.UserID = objPostDM.UserID;
        objProductEntity.User = null;

        List <ProductImageFile> objListFileEntity = MapProductViweModelToProductFileEntity(objPostDM);

        if (objPostDM != null)
        {
            objProductEntity.ListImageFiles = objListFileEntity;
            objProductEntity.ListComments = new List<ProductComment>();

            _Context.Products.Add( objProductEntity );
        }

        int result = await _Context.SaveChangesAsync();

        return result > 0;
    }

    private Product MapProductViweModelToProductEntity ( Product productDM )
    {
        return new Product ( )
        {
            ProductName = productDM.ProductName,
            SearchTag = 
                string.IsNullOrWhiteSpace( productDM.SearchTag ) 
                    ? null 
                    : productDM.SearchTag,

            //Price = productDM.UnitPrice,
            Discount = productDM.Discount,
            SaleCommission = productDM.SaleCommission,
            CategoryID = productDM.CategoryID,
            SubCategoryID = productDM.SubCategoryID,
            Description = 
                string.IsNullOrWhiteSpace ( productDM.Description) 
                ? null 
                : productDM.Description,

            PostType = EnumPostType.Product
        };
    }

    private List<ProductImageFile> MapProductViweModelToProductFileEntity 
        ( Product productFileDM )
    {
        List<ProductImageFile> objListFileEntity = new List<ProductImageFile>();
        productFileDM.ImageFiles.ForEach ( fileVM =>
        {
            objListFileEntity.Add ( new ProductImageFile ( fileVM.ImageFileContent ) );
        } );
        return objListFileEntity;
    }

    public async Task<bool> UpdateProduct ( Product objPostDm )
    {
        var product = await _Context.Products.SingleAsync<Product>
             (a => a.ProductID == objPostDm.ProductID);

        //product.ModifyBaseData ( objPostDm.ModelBase );

        product.UserID = objPostDm.UserID;
        product.User = null;


        List<ProductImageFile> images = new List<ProductImageFile>();

        images.AddRange ( product.ListImageFiles );

        objPostDm.ListImageFiles.ForEach ( fileDM =>
        {
            var objFile = new ProductImageFile(fileDM.ImageFileContent);
            objFile.ProductID = product.ProductID;
            images.Add ( objFile );
        } );


        List<ProductComment> comments = new List<ProductComment>();

        comments.AddRange ( product.ListComments );

        objPostDm.ListComments.ForEach ( commentVM =>
        {
            var objComment = new ProductComment();
            objComment.ProductID = product.ProductID;
            objComment.Comment = commentVM.Comment;
            comments.Add ( objComment );
        } );

        product.ProductName = objPostDm.ProductName;
        product.Discount = objPostDm.Discount;
        product.SaleCommission = objPostDm.SaleCommission;
        product.SearchTag = objPostDm.SearchTag;
        product.PostType = EnumPostType.Product;
        product.Description = objPostDm.Description;
        product.CategoryID = objPostDm.CategoryID;
        product.SubCategoryID = objPostDm.SubCategoryID;
        product.Price = objPostDm.UnitPrice;
        product.ListComments = comments;
        product.ListImageFiles = images;

        _Context.Products.Update ( product );

        var result = await _Context.SaveChangesAsync ();

        return result > 0;
    }

}

