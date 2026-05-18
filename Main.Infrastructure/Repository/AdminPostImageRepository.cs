using BusinessModel;
using Data;
using Entity.Model;
using IRepository;
using Main.Common.Enums;
using Microsoft.EntityFrameworkCore;

namespace Repository;

public class AdminPostImageRepository: IAdminPostImageRepository
{
    private readonly BussinessAppDbContext _context;

    public AdminPostImageRepository( 
        BussinessAppDbContext context ) 
    { 
        _context = context;
    }


    public async Task<List<PanelPostDataModel>> 
        GetSelectAdminPosts(EnumCompanyName company)
    {
        List<AdminPost> listAdminPost = 
              await _context.AdminPosts
                    .Where(a => a.HostCompanyName == company)
                    .ToListAsync<AdminPost>();

        if ( listAdminPost == null )
        {
            return new List<PanelPostDataModel> ( );
        }

        List<PanelPostDataModel> listSelectPanelPostDM 
            = new List<PanelPostDataModel>();

        PanelPostDataModel objDM;

        listAdminPost.ForEach ( entity => 
        {
            entity.ListAdminImageFiles.ToList ( ).ForEach ( file =>
            {
                objDM = new PanelPostDataModel ( );

                objDM.RootID = entity.AdminPostID;
                objDM.EnumPostType = entity.PostType;
                objDM.PostTitle = entity.Title;
                objDM.ImageFileContent = file.ImageFileContent;

                listSelectPanelPostDM.Add ( objDM );
            } );

        } );

        return listSelectPanelPostDM
            .OrderBy ( a => a.PostTitle )
            .ToList<PanelPostDataModel> ( );
    }
}

