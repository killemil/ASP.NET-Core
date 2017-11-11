namespace CarDealer.Services.Models.Logs
{
    using CarDealer.Data.Models.Enums;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class LogListingModel
    {
        [Required]
        [MaxLength(60)]
        public string Username { get; set; }

        public Operation Operation { get; set; }

        [Required]
        [MaxLength(60)]
        public string Table { get; set; }

        public DateTime Time { get; set; }
    }
}
