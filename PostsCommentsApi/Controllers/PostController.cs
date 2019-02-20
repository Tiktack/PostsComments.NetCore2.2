using BusinessLayer;
using DataLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using PostsCommentsApi.DTO;

// ReSharper disable once IdentifierTypo
namespace PostsCommentsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;
        private readonly ILogger<PostController> _logger;
        private readonly IMapper _mapper;

        public PostController(IPostService postService, ILogger<PostController> logger, IMapper mapper)
        {
            _postService = postService;
            _logger = logger;
            _mapper = mapper;
        }

        // GET: api/Post
        [HttpGet]
        public async Task<IEnumerable<ViewPostDTO>> Get()
        {
            var posts = await _postService.GetAllPosts();
            return posts.Select(post => _mapper.Map<ViewPostDTO>(post));
        }

        // GET: api/Post/{id}
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
