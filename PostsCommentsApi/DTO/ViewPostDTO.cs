using System;

namespace PostsCommentsApi.DTO
{
    public class ViewPostDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string Author { get; set; }
        public DateTime DateTime { get; set; }
    }
}
