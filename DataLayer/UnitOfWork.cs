using System;
using System.Threading.Tasks;
using DataLayer.Interfaces;
using DataLayer.Interfaces.Repositories;

namespace DataLayer
{
    public sealed class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly BaseContext _context;
        private bool _disposed;

        public UnitOfWork(ICommentRepository commentRepository, IPostRepository postRepository, BaseContext context)
        {
            CommentRepository = commentRepository;
            PostRepository = postRepository;
            _context = context;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public ICommentRepository CommentRepository { get; set; }
        public IPostRepository PostRepository { get; set; }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        private void Dispose(bool disposing)
        {
            if (_disposed) return;
            if (disposing) _context.Dispose();

            _disposed = true;
        }
    }
}