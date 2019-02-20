using DataLayer.Entities;
using DataLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class PostService : IPostService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PostService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddPost(Post post)
        {
            await _unitOfWork.PostRepository.Insert(post);
        }

        public async Task<Post> GetPost(int id)
        {
            if (id < 1)
                throw new ArgumentException();
            return await _unitOfWork.PostRepository.GetById(id) ?? throw new ArgumentException();
        }

        public async Task<IEnumerable<Post>> GetAllPosts()
        {
            return await _unitOfWork.PostRepository.GetAll();
        }

        public async Task UpdatePost(Post post, int id)
        {
            var item = await _unitOfWork.PostRepository.GetById(id);
            if (item == null)
                throw new ArgumentException($"Can not find post with id = {id}");
            post.Id = id;
            post.Author = item.Author;
            post.DateTime = item.DateTime;
            await _unitOfWork.PostRepository.Update(post);
        }

        public async Task DeletePost(int id)
        {
            await _unitOfWork.PostRepository.Delete(id);
        }
    }
}
