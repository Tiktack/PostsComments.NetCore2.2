using Microsoft.AspNetCore.Identity;

namespace Identity.DataLayer
{
    public class User : IdentityUser
    {
        public int Year { get; set; }
    }
}