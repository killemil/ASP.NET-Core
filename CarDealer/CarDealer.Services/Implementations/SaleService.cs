namespace CarDealer.Services.Implementations
{
    using System.Collections.Generic;
    using CarDealer.Services.Models.Sales;
    using CarDealer.Data;
    using System.Linq;
    using CarDealer.Services.Models.Cars;
    using CarDealer.Services.Models.Customers;
    using System;

    public class SaleService : ISaleService
    {
        private readonly CarDealerDbContext db;

        public SaleService(CarDealerDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<SaleModel> All(bool hasDiscount, double? discountPercent)
        {
            var salesQuery = this.db.Sales.AsQueryable();

            if (hasDiscount)
            {
                salesQuery = salesQuery.Where(s => s.Discount > 0);
            }

            if (discountPercent != null)
            {
                salesQuery = salesQuery.Where(s => s.Discount == discountPercent);
            }

            return salesQuery.Select(s => new SaleModel
                {
                    Id = s.Id,
                    Discount = s.Discount,
                    Car = new CarModel
                    {
                        Make = s.Car.Make,
                        Model = s.Car.Model,
                        TravelledDistance = s.Car.TravelledDistance
                    },
                    Customer = new CustomerModel
                    {
                        Name = s.Customer.Name,
                        BirthDate = s.Customer.BirthDate,
                        IsYoungDriver = s.Customer.IsYoungDriver
                    },
                    PriceWithoutDiscount = s.Car.Parts.Sum(p => p.Part.Price)
                })
                .ToList();
        }

        public SaleByIdModel ById(int id)
            => this.db.Sales.Where(s => s.Id == id)
                .Select(s => new SaleByIdModel
                {
                    CarMake = s.Car.Make,
                    CarModel = s.Car.Model,
                    CustomerName = s.Customer.Name
                })
                .FirstOrDefault();
    }
}
