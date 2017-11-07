namespace CarDelaer.Web.Controllers
{
    using CarDealer.Services;
    using CarDealer.Services.Models;
    using CarDealer.Services.Models.Customers;
    using Microsoft.AspNetCore.Mvc;
    using Models.Customers;
    
    [Route("customers")]
    public class CustomersController : Controller
    {
        private readonly ICustomerService customers;

        public CustomersController(ICustomerService customers)
        {
            this.customers = customers;
        }

        [Route("create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Route("create")]
        public IActionResult Create(CustomerCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            this.customers.Create(model.Name, model.BirthDate);

            return Redirect("/");
        }

        [Route("all/{order}")]
        public IActionResult All(string order)
        {
            var orderDirection = order.ToLower() == "ascending"
                ? OrderDirection.Ascending
                : OrderDirection.Descending;

            var allCustomers = this.customers.Ordered(orderDirection);

            return View(new AllCustomersModel
            {
                Customers = allCustomers,
                OrderDirection = orderDirection
            });
        }

        [Route("{id}")]
        public IActionResult Details(int id)
        {
            return View(this.customers.WithSalesById(id));
        }
    }
}
