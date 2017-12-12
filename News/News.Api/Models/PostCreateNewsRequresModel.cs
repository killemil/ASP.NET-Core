namespace News.Api.Models
{
    using System.ComponentModel.DataAnnotations;

    public class PostCreateNewsRequresModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }
    }
}
