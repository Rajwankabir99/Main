using Microsoft.EntityFrameworkCore;
using Main.Model.Repository;
using Main.Model;
using Main.Common;
namespace Main.Infrastructure.Repository;

public class AdminPostImageRepository: IAdminPostImageRepository
{

    private readonly WebBusinessEntityContext _context;

    public AdminPostImageRepository(WebBusinessEntityContext context) 
    { 
        _context = context;
    }


    public async Task<List<AdminPost>> GetSelectAdminPosts(EnumCompanyName company)
    {
        List<AdminPost> list = await _context.AdminPosts
                                        .Where(a => a.HostCompanyName == company)
                                        .ToListAsync<AdminPost>();

        return list;
    }
}

