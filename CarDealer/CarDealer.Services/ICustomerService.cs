namespace CarDealer.Services
{
    using CarDealer.Services.Models;
    using CarDealer.Services.Models.Customers;
    using System;
    using System.Collections.Generic;

    public interface ICustomerService
    {
        IEnumerable<CustomerModel> Ordered(OrderDirection order);

        IEnumerable<CustomerSelectModel> All();

        CustomerWithSalesModel WithSalesById(int id);

        void Create(string name, DateTime birthDate, bool isYoungDriver);

        CustomerFormModel ById(int id);
        
        bool Exist(int id);

        void Edit(int id, string name, DateTime birthDate, bool isYoungDriver);
    }
}
