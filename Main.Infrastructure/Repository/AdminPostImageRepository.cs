using Microsoft.EntityFrameworkCore;
using IRepository;
using Main.Model;
using Main.Common;
using Data;

namespace Repository;

public class AdminPostImageRepository: IAdminPostImageRepository
{

    private readonly AppDbContext _context;

    public AdminPostImageRepository( AppDbContext context ) 
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

