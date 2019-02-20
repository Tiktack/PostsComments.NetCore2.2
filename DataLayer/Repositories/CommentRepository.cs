using DataLayer.Entities;
using DataLayer.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataLayer.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly BaseContext _context;

        public CommentRepository(BaseContext context)
        {
            _context = context;
        }

        public async Task Insert(Comment item)
        {
            await _context.Comments.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var comment = await _context.Comments.FindAsync(id);
            if (comment == null)
                throw new ArgumentException($"Can not find comment with id {id}");
            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Comment item)
        {
            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<Comment> GetById(int id)
        {
            return await _context.Comments.AsNoTracking().SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Comment>> GetAll()
        {
            return await _context.Comments.AsNoTracking().ToListAsync();
        }
    }
}
