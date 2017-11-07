namespace CarDealer.Services.Implementations
{
    using CarDealer.Data;
    using Models;
    using Models.Customers;
    using System.Collections.Generic;
    using System.Linq;
    using System;
    using CarDealer.Data.Models;

    public class CustomerService : ICustomerService
    {
        private readonly CarDealerDbContext db;

        public CustomerService(CarDealerDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<CustomerModel> Ordered(OrderDirection order)
        {
            var customersQuery = this.db.Customers.AsQueryable();

            switch (order)
            {
                case OrderDirection.Ascending:
                    customersQuery = customersQuery
                        .OrderBy(c => c.BirthDate)
                        .ThenBy(c => c.IsYoungDriver == false);
                    break;
                case OrderDirection.Descending:
                    customersQuery = customersQuery
                        .OrderByDescending(c => c.BirthDate)
                        .ThenBy(c => c.IsYoungDriver == false);
                    break;
                default:
                    throw new InvalidOperationException($"Invalid order direction: {order}.");
            }

            return customersQuery
                    .Select(c => new CustomerModel
                    {
                        Name = c.Name,
                        IsYoungDriver = c.IsYoungDriver,
                        BirthDate = c.BirthDate
                    })
                    .ToList();
        }

        public CustomerWithSalesModel WithSalesById(int id)
        {
            var customer = this.db
                .Customers
                .Where(c => c.Id == id)
                .Select(c => new CustomerWithSalesModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    BoughtCars = c.Sales.Count,
                    TotalSpentMoney = c.Sales.Sum(s => s.Car.Parts.Sum(p => p.Part.Price))
                })
                .FirstOrDefault();

            if (customer == null)
            {
                throw new InvalidOperationException($"Can not find customer with id {id}");
            }

            return customer;
        }

        public void Create(string name, DateTime birthDate)
        {
            var customer = new Customer
            {
                Name = name,
                BirthDate = birthDate,
                IsYoungDriver = DateTime.Now.Year - birthDate.Year < 2 ? true : false
            };

            db.Customers.Add(customer);
            db.SaveChanges();
        }
    }
}
