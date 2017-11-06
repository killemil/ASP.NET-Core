﻿namespace CarDealer.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Car
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(60)]
        public string Make { get; set; }

        [Required]
        [MaxLength(60)]
        public string Model { get; set; }

        [Range(0, long.MaxValue)]
        public long TravelledDistance { get; set; }

        public ICollection<Sale> Sales { get; set; } = new HashSet<Sale>();

        public ICollection<PartCar> Parts { get; set; } = new HashSet<PartCar>();
    }
}
