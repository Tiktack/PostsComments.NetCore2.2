using DataLayer.Entities;
using DataLayer.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataLayer
{
    public class BaseContext : DbContext, IBaseContext
    {
        public BaseContext(DbContextOptions<BaseContext> options)
            : base(options) { }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}
