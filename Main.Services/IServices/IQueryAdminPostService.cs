using DataTransferModel;

namespace Application.Service;

public interface IQueryAdminPostService
{
    Task<List<AdminPostDataModel>> GetAllAdminPosts ( );

    Task<AdminPostDataModel>
                 GetAdminPostForEditPostID ( int postID );
}

