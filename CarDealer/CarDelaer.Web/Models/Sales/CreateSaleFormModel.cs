namespace CarDealer.Web.Models.Sales
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class CreateSaleFormModel
    {
        [Display(Name ="Customer")]
        public int CustomerId { get; set; }

        [Display(Name = "Car")]
        public int CarId { get; set; }

        public double Discount { get; set; }

        public IEnumerable<SelectListItem> Customers { get; set; }

        public IEnumerable<SelectListItem> Cars { get; set; }
    }
}
