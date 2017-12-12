namespace News.Api.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class PutEditNewsRequestModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        public DateTime PublishedDate{ get; set; }
    }
}
