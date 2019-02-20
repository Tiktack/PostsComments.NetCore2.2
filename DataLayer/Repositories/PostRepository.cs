using DataLayer.Entities;
using DataLayer.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataLayer.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly BaseContext _context;

        public PostRepository(BaseContext context)
        {
            _context = context;
        }
        public async Task Insert(Post item)
        {
            await _context.Posts.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post == null)
                throw new ArgumentException($"Can not find post with id {id}");
            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Post item)
        {
            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<Post> GetById(int id)
        {
            return await _context.Posts.AsNoTracking().SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Post>> GetAll()
        {
            return await _context.Posts.AsNoTracking().ToListAsync();
        }
    }
}
