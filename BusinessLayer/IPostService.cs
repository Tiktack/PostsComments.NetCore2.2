using DataLayer.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public interface IPostService
    {
        Task AddPost(Post post);
        Task<Post> GetPost(int id);
        Task<IEnumerable<Post>> GetAllPosts();
        Task UpdatePost(Post post, int id);
        Task DeletePost(int id);
    }
}
