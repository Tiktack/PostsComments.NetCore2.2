using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Controllers
{
    public interface IIdentityContract
    {
        [HttpGet]
        [Route("")]
        Task<string> Get();

        [HttpPost]
        [Route("")]
        Task<string> GetNew();
    }
}