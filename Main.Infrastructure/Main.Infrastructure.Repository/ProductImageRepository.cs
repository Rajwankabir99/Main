using Microsoft.EntityFrameworkCore;
using Main.Model.Repository;
using Main.Model;
using Main.Common;
namespace Main.Infrastructure.Repository;

public class ProductImageRepository : IProductImageRepository
{
    private readonly WebBusinessEntityContext _context;

    public ProductImageRepository(WebBusinessEntityContext context)
    {
        _context = context;
    }

    public async Task<List<Product>> GetSelectProducts(EnumCompanyName company) 
    {
            
        List<Product> list = await _context.Products
                                    .Where( a => a.HostCompanyName == company)
                                    .ToListAsync();
            
        return list;

    }
}

 