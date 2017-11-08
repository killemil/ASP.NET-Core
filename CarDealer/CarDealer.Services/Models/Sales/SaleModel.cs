namespace CarDealer.Services.Models.Sales
{
    using Models.Cars;
    using Models.Customers;
    using System;

    public class SaleModel
    {
        public int Id { get; set; }

        public CarModel Car { get; set; }

        public CustomerModel Customer { get; set; }

        public decimal PriceWithDiscount => this.PriceWithoutDiscount - (this.PriceWithoutDiscount * Convert.ToDecimal(this.Discount + (this.Customer.IsYoungDriver ? 0.05 : 0)));
        
        public decimal PriceWithoutDiscount { get; set; }

        public double Discount { get; set; }
    }
}