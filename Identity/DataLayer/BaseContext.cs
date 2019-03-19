using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Identity.DataLayer
{
    public class BaseContext : IdentityDbContext<User>
    {
        public BaseContext(DbContextOptions<BaseContext> options)
            : base(options)
        {
        }
    }
}