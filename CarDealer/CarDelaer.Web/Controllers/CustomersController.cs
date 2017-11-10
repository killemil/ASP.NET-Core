namespace CarDealer.Web.Controllers
{
    using Infrastructure.Extensions;
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
        public IActionResult Create(CustomerFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            this.customers.Create(
                model.Name,
                model.BirthDate,
                model.IsYoungDriver);

            return RedirectToAction(nameof(All), new { order = OrderDirection.Ascending });
        }

        [Route(nameof(Edit) + "/{id}")]
        public IActionResult Edit(int id)
        {
            var customer = this.customers.ById(id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        [HttpPost]
        [Route(nameof(Edit) + "/{id}")]
        public IActionResult Edit(int id, CustomerFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (!this.customers.Exist(id))
            {
                return NotFound();
            }

            this.customers.Edit(id, model.Name, model.BirthDate, model.IsYoungDriver);

            return RedirectToAction(nameof(All), new { order = OrderDirection.Ascending });
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
            return this.ViewOrNotFound(this.customers.WithSalesById(id));
        }
    }
}
