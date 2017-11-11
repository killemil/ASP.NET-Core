namespace CarDealer.Web.Models.Sales
{
    using System.ComponentModel.DataAnnotations;

    public class CreateSaleConfirmFormModel
    {
        public int CustomerId { get; set; }

        [Required]
        [Display(Name = "Customer")]
        public string CustomerName { get; set; }

        public int CarId { get; set; }

        [Required]
        [Display(Name = "Car")]
        public string CarName { get; set; }

        public double Discount { get; set; }

        [Display(Name = "Car Price")]
        public decimal Price { get; set; }

        [Display(Name = "Final Car Price")]
        public decimal FinalPrice { get; set; }
    }
}
