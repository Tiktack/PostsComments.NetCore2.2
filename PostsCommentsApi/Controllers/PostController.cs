using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLayer;
using CommonHttp.Interfaces;
using DataLayer.Entities;
using Identity.Controllers;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PostsCommentsApi.DTO;

// ReSharper disable once IdentifierTypo
namespace PostsCommentsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ODataController
    {
        private readonly IIdentityContract _identity;
        private readonly ILogger<PostController> _logger;
        private readonly IMapper _mapper;
        private readonly IPostService _postService;

        public PostController(IPostService postService, IMapper mapper, ILogger<PostController> logger,
            IGeneratorWebServiceProxy<IIdentityContract> identity)
        {
            _postService = postService;
            _mapper = mapper;
            _logger = logger;
            _identity = identity.GetWebService();
        }

        // GET: api/Post
        [HttpGet]
        [EnableQuery]
        public async Task<IEnumerable<ViewPostDTO>> Get()
        {
            //var a = await _identity.GetNew();
            var posts = await _postService.GetAllPosts();
            return posts.Select(post => _mapper.Map<ViewPostDTO>(post));
        }

        // GET: api/Post/{id}
        [EnableQuery]
        [HttpGet("{id}")]
        public async Task<ViewPostDTO> Get(int id)
        {
            var post = await _postService.GetPost(id);
            return _mapper.Map<ViewPostDTO>(post);
        }

        // POST: api/Post
        [HttpPost]
        public async Task Post([FromBody] InsertPostDTO item)
        {
            var post = _mapper.Map<Post>(item);
            await _postService.AddPost(post);
        }

        // PUT: api/Post/5
        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody] EditPostDTO item)
        {
            var post = _mapper.Map<Post>(item);
            await _postService.UpdatePost(post, id);
        }

        // DELETE: api/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _postService.DeletePost(id);
        }
    }
}