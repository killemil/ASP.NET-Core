namespace CarDelaer.Web.Models.Customers
{
    using CarDealer.Services.Models;
    using CarDealer.Services.Models.Customers;
    using System.Collections.Generic;

    public class AllCustomersModel
    {
        public OrderDirection OrderDirection { get; set; }

        public IEnumerable<CustomerModel> Customers { get; set; }
    }
}
