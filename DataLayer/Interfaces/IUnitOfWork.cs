using System.Threading.Tasks;
using DataLayer.Interfaces.Repositories;

namespace DataLayer.Interfaces
{
    public interface IUnitOfWork
    {
        ICommentRepository CommentRepository { get; set; }
        IPostRepository PostRepository { get; set; }
        Task SaveAsync();
    }
}