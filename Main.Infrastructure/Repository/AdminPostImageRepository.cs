using Data;
using IRepository;
using Main.Common.Enums;
using Microsoft.EntityFrameworkCore;
using Domain.Model;

namespace Repository;

public class AdminPostImageRepository: IAdminPostImageRepository
{
    private readonly BussinessAppDbContext _context;

    public AdminPostImageRepository( 
        BussinessAppDbContext context ) 
    { 
        _context = context;
    }


    public async Task<List<PanelPost>> 
        GetSelectAdminPosts(EnumCompanyName company)
    {
        List<AdminPost> listAdminPost = 
              await _context.AdminPosts
                    .Where(a => a.HostCompanyName == company)
                    .ToListAsync<AdminPost>();

        if ( listAdminPost == null )
        {
            return new List<PanelPost> ( );
        }

        List<PanelPost> listSelectPanelPostDM 
            = new List<PanelPost>();

        PanelPost objDM;

        listAdminPost.ForEach ( entity => 
        {
            entity.ListAdminImageFiles.ToList ( ).ForEach ( file =>
            {
                objDM = new PanelPost ( );

                objDM.RootID = entity.AdminPostID;
                objDM.EnumPostType = entity.PostType;
                objDM.PostTitle = entity.Title;
                objDM.ImageFileContent = file.ImageFileContent;

                listSelectPanelPostDM.Add ( objDM );
            } );

        } );

        return listSelectPanelPostDM
            .OrderBy ( a => a.PostTitle )
            .ToList<PanelPost> ( );
    }
}

