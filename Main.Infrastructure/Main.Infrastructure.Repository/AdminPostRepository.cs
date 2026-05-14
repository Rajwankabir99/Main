using Microsoft.EntityFrameworkCore;
using Main.Model.Repository;
using Main.Model;
namespace Main.Infrastructure.Repository;

public class AdminPostRepository : IAdminPostRepository
{
    private readonly WebBusinessEntityContext _Context;

    public AdminPostRepository(WebBusinessEntityContext context)
    {
        _Context = context;
    }

    public async Task<bool> SaveChanges()
    {
        var result = await _Context.SaveChangesAsync();
        return result > 0;
    }

    public async Task<List<AdminPost>> GetAllAdminContentPosts()
    {
        return await _Context.AdminPosts.ToListAsync();
    }

    public async Task<bool> DeleteAdminPost(int postId)
    {
        var post = _Context.AdminPosts.ToList().Single<AdminPost>(a => a.AdminPostID == postId);

        if (post != null)
        {
            _Context.AdminPosts.Remove(post);
        }
                
        var result = await _Context.SaveChangesAsync();
        return result > 0;
    }

    public async Task<bool> DeleteAdminPostImage(int id, int postId)
    {
        var image = await _Context.AdminImageFiles.Where(a => a.AdminImageFileID == id && a.AdminPostID == postId).FirstOrDefaultAsync();

        if (image != null)
        {
            _Context.AdminImageFiles.Remove(image);
        }

        var result = await _Context.SaveChangesAsync();
        return result > 0;
    }

    public async Task<AdminPost> GetAdminPostByPostID(int postId)
    {
        var post = await _Context.AdminPosts.SingleAsync<AdminPost>(a => a.AdminPostID == postId);
        return post;
    }

    public async Task<bool> SaveNewAdminPost(AdminPost postObject, List<AdminImageFile> objListFiles)
    {
        if(postObject != null)
        {
            postObject.ListAdminImageFiles = objListFiles;
            postObject.ListAdminPostComments = new List<AdminPostComment>();
            _Context.AdminPosts.Add(postObject);
        }
            
        int result = await _Context.SaveChangesAsync();
        return result > 0;
    }
}

