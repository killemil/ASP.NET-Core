namespace CarDealer.Web.Models.Cars
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class CarFormModel
    {
        [Required]
        [MaxLength(60)]
        public string Make { get; set; }

        [Required]
        [MaxLength(60)]
        public string Model { get; set; }

        [Display(Name ="Travelled Distance")]
        [Range(0, long.MaxValue,ErrorMessage = "Travelled distance must be positive number.")]
        public long TravelledDistance { get; set; }

        [Display(Name = "Parts")]
        public IEnumerable<int> SelectedParts { get; set; } = new List<int>();

        public IEnumerable<SelectListItem> Parts { get; set; }
    }
}
