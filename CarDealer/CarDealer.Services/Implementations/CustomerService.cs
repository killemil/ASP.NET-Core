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
                        Id = c.Id,
                        Name = c.Name,
                        IsYoungDriver = c.IsYoungDriver,
                        BirthDate = c.BirthDate
                    })
                    .ToList();
        }

        public IEnumerable<CustomerSelectModel> All()
                => this.db.Customers
                    .OrderBy(c => c.Name)
                    .Select(c => new CustomerSelectModel
                    {
                        Id = c.Id,
                        Name = c.Name,
                    })
                    .ToList();

        public CustomerWithSalesModel WithSalesById(int id)
            => this.db
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

        public void Create(string name, DateTime birthDate, bool isYoungDriver)
        {
            var customer = new Customer
            {
                Name = name,
                BirthDate = birthDate,
                IsYoungDriver = isYoungDriver
            };

            db.Customers.Add(customer);
            db.SaveChanges();
        }

        public CustomerFormModel ById(int id)
            => this.db.Customers.Where(c => c.Id == id)
                .Select(c => new CustomerFormModel
                {
                    Name = c.Name,
                    BirthDate = c.BirthDate,
                    IsYoungDriver = c.IsYoungDriver
                })
                .FirstOrDefault();

        public bool Exist(int id)
            => this.db.Customers.Any(c => c.Id == id);

        public void Edit(int id, string name, DateTime birthDate, bool isYoungDriver)
        {
            var customer = this.db.Customers.Find(id);
            customer.Name = name;
            customer.BirthDate = birthDate;
            customer.IsYoungDriver = isYoungDriver;

            db.SaveChanges();
        }
    }
}
