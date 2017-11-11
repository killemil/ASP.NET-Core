namespace CarDealer.Data.Models
{
    using Enums;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Log
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(60)]
        public string Username { get; set; }

        [Required]
        [MaxLength(60)]
        public string Table { get; set; }

        [Required]
        public Operation Operation { get; set; }

        public DateTime Modified { get; set; }
    }
}
