using DataLayer.Interfaces.Repositories;
using System.Threading.Tasks;

namespace DataLayer.Interfaces
{
    public interface IUnitOfWork
    {
        ICommentRepository CommentRepository { get; set; }
        IPostRepository PostRepository { get; set; }
        Task SaveAsync();
    }
}
