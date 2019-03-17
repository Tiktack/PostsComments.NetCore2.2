using AutoMapper;
using DataLayer.Entities;
using PostsCommentsApi.DTO;

namespace PostsCommentsApi.Mapping
{
    public class PostMappers : Profile
    {
        public PostMappers()
        {
            CreateMap<InsertPostDTO, Post>();
            CreateMap<Post, ViewPostDTO>();
            CreateMap<EditPostDTO, Post>();
        }
    }
}