namespace CarDealer.Services.Models.Sales
{
    using Models.Cars;
    using Models.Customers;

    public class SaleModel
    {
        public CarModel Cars { get; set; }

        public CustomerModel Customer { get; set; }

        public decimal PriceWithDiscount { get; set; }

        public decimal PriceWithoutDiscount { get; set; }

        public double Discount { get; set; }
    }
}
