namespace CarDealer.Services.Models.Customers
{
    public class CustomerWithSalesModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int BoughtCars { get; set; }

        public decimal TotalSpentMoney { get; set; }
    }
}
