namespace LearningSystem.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class User : IdentityUser
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public DateTime BirthDate { get; set; }

        public ICollection<UserCourse> Courses { get; set; } = new HashSet<UserCourse>();

        public ICollection<Article> Articles { get; set; } = new HashSet<Article>();
    }
}
