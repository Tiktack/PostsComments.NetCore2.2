using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common;
using DataLayer.Entities;
using DataLayer.Interfaces;
using Microsoft.Extensions.Caching.Distributed;

namespace BusinessLayer
{
    public class PostService : IPostService
    {
        private readonly IDistributedCache _distributedCache;
        private readonly IUnitOfWork _unitOfWork;

        public PostService(IUnitOfWork unitOfWork, IDistributedCache distributedCache)
        {
            _unitOfWork = unitOfWork;
            _distributedCache = distributedCache;
        }

        public async Task AddPost(Post post)
        {
            await _unitOfWork.PostRepository.Insert(post);
        }

        public async Task<Post> GetPost(int id)
        {
            if (id < 1)
                throw new ArgumentException();

            var cacheKey = $"post:{id}";
            var cacheValue = await _distributedCache.GetAsync(cacheKey);
            if (cacheValue != null) return cacheValue.FromByteArray<Post>();
            var result = await _unitOfWork.PostRepository.GetById(id) ?? throw new ArgumentException();
            await _distributedCache.SetAsync(cacheKey, result.ToByteArray(),
                new DistributedCacheEntryOptions {AbsoluteExpiration = DateTime.Now.AddMinutes(2)});
            return result;
        }

        public async Task<IEnumerable<Post>> GetAllPosts()
        {
            const string cacheKey = "allPosts";
            var cacheValue = await _distributedCache.GetAsync(cacheKey);
            if (cacheValue != null) return cacheValue.FromByteArray<IEnumerable<Post>>();
            var result = await _unitOfWork.PostRepository.GetAll();
            await _distributedCache.SetAsync(cacheKey, result.ToByteArray(),
                new DistributedCacheEntryOptions {AbsoluteExpiration = DateTime.Now.AddMinutes(2)});
            return result;
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