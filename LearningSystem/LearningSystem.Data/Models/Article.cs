namespace LearningSystem.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Article
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        [MinLength(3)]
        public string Title { get; set; }

        [Required]
        [MaxLength(2000)]
        [MinLength(3)]
        public string Content { get; set; }

        public DateTime PublishedDate { get; set; }

        public string AuthorId { get; set; }

        public User Author { get; set; }
    }
}
