using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase, IIdentityContract
    {
        // GET api/values

        [HttpGet]
        [Route("")]
        public async Task<string> Get()
        {
            await Task.Delay(1000);
            return "hello from identity";
        }

        [HttpPost]
        [Route("")]
        public async Task<string> GetNew()
        {
            await Task.Delay(1000);
            return "post";
        }
    }
}