using Main.Common;
using Data;
using Microsoft.EntityFrameworkCore;
using Main.Infrastructure;


namespace Main.Model.Repository
{
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
}
