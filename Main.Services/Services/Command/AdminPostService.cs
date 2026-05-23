using DataTransferModel;
using Domain.Model;
using IRepository;
using Services.Extensions;

namespace Application.Service;

public class AdminPostDataService: 
    ICommandAdminPostService, IQueryAdminPostService
{
    private readonly IAdminPostRepository _AdminPostRepository;
    
    public AdminPostDataService 
           ( IAdminPostRepository adminPostRepository )
    {
        _AdminPostRepository = adminPostRepository;
    }

    public async Task<List<AdminPostDataModel>> GetAllAdminPosts ( )
    {
        var listadminPostEntities = await _AdminPostRepository.GetAllAdminContentPosts();

        List<AdminPostDataModel> listPostDataModel =
            AdminPostServiceMappings.MapListDataModel ( listadminPostEntities );

        return listPostDataModel;
    }
    

    public async Task<bool> SaveNewAdminPost ( 
                 AdminPostDataModel objAdminPostDm )
    {
        AdminPost adminPostEntity 
            = AdminPostServiceMappings.MapAdminPostEntity
            ( objAdminPostDm, objAdminPostDm.ListAdminPostFileImages );
         
        return await _AdminPostRepository
            .SaveNewAdminPost ( adminPostEntity );
    }


    public async Task<AdminPostDataModel> GetAdminPostForEditPostID ( int postID )
    {
        var postEntity = await _AdminPostRepository
                    .GetAdminPostByPostID(postID);


        AdminPostDataModel objAdminPostDataModel =
            AdminPostServiceMappings.MapAdminPostDataModel (postEntity);

        return objAdminPostDataModel;
    }


    public async Task<bool> UpdateAdminPost ( AdminPostDataModel adminPostDataModel )
    {
        AdminPost adminPostEntity = await _AdminPostRepository
                    .GetAdminPostByPostID(adminPostDataModel.AdminPostID);

        if ( adminPostEntity == null )
        {
            return false;
        }

        adminPostEntity = AdminPostServiceMappings
                          .UpdateAdminPostEntityMapping( adminPostEntity,adminPostDataModel );


        return await _AdminPostRepository
               .UpdateAdminPost( adminPostEntity );
    }
    

    public async Task<bool> DeleteAdminPostImage ( int id,int postId )
    {
        return await _AdminPostRepository
                .DeleteAdminPostImage ( id, postId );
    }


    public async Task<bool> DeleteAdminPost 
                 ( int postId )
    {
        return await _AdminPostRepository
                .DeleteAdminPost(postId);
    }
}