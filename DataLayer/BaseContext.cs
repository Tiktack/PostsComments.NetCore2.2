using DataLayer.Entities;
using DataLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataLayer
{
    public class BaseContext : DbContext, IBaseContext
    {
        private readonly IConfiguration _configuration;

        public BaseContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration.GetSection("ConnectionString").Value);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

    }
}
