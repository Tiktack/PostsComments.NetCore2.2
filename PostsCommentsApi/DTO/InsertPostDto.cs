using System;
using System.ComponentModel.DataAnnotations;

namespace PostsCommentsApi.DTO
{
    public class InsertPostDTO
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Text { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public DateTime DateTime { get; set; }
    }
}
