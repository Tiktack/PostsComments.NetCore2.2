using AutoMapper;
using BusinessLayer;
using DataLayer.Entities;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using PostsCommentsApi.DTO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

// ReSharper disable once IdentifierTypo
namespace PostsCommentsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ODataController
    {
        private readonly IPostService _postService;
        private readonly IMapper _mapper;
        private readonly ILogger<PostController> _logger;

        public PostController(IPostService postService, IMapper mapper, ILogger<PostController> logger)
        {
            _postService = postService;
            _mapper = mapper;
            _logger = logger;
        }

        // GET: api/Post
        [HttpGet]
        [EnableQuery]
        public async Task<IEnumerable<ViewPostDTO>> Get()
        {
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
