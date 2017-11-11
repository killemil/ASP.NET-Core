namespace CarDealer.Services.Models.Customers
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class CustomerFormModel
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public DateTime BirthDate { get; set; }

        [Display(Name = "Young Driver")]
        public bool IsYoungDriver { get; set; }
    }
}
