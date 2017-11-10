namespace CarDealer.Web.Controllers
{
    using CarDealer.Services;
    using CarDealer.Web.Models.Sales;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;
    using System.Linq;

    [Route("sales")]
    public class SalesController : Controller
    {
        private readonly ISaleService sales;
        private readonly ICarService cars;
        private readonly ICustomerService customers;

        public SalesController(
            ISaleService sales,
            ICarService cars,
            ICustomerService customers)
        {
            this.sales = sales;
            this.cars = cars;
            this.customers = customers;
        }

        public IActionResult All()
        {
            return View(this.sales.All(false, null));
        }

        [Route("{id}")]
        public IActionResult Details(int id)
        {
            return View(this.sales.ById(id));
        }

        [Route("discounted")]
        public IActionResult Discounted()
        {

            return View(this.sales.All(true, null));
        }

        [Route("discounted/{percent}")]
        public IActionResult Discounted(double percent)
        {
            return View(this.sales.All(true, percent));
        }

        [Authorize]
        [Route("create")]
        public IActionResult Create()
        {
            return View(new CreateSaleFormModel
            {
                Customers = this.GetCustomersListItems(),
                Cars = this.GetCarsListItems()
            });
        }

        [Authorize]
        [Route("ConfirmCreate")]
        public IActionResult ConfirmCreate(CreateSaleFormModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Customers = this.GetCustomersListItems();
                model.Cars = this.GetCarsListItems();
                return View(nameof(Create),model);
            }

            var customer = this.customers.ById(model.CustomerId);
            var car = this.cars.ById(model.CarId);
            double discount = ((customer.IsYoungDriver == true ? 5 : 0) + model.Discount) / 100;

            return View(new CreateSaleConfirmFormModel
            {
                CarId = model.CarId,
                CustomerId = model.CustomerId,
                Discount = discount * 100,
                CarName = car.CarName,
                CustomerName = customer.Name,
                Price = car.Price,
                FinalPrice = car.Price - (car.Price * (decimal)discount)
            });
        }

        [Authorize]
        [HttpPost]
        [Route("ConfirmCreate")]
        public IActionResult ConfirmCreate(CreateSaleConfirmFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Create));
            }

            this.sales.Create(model.CustomerId, model.CarId, model.Discount);

            return RedirectToAction(nameof(All));
        }

        private IEnumerable<SelectListItem> GetCustomersListItems()
            => this.customers.All().Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString()
            });

        private IEnumerable<SelectListItem> GetCarsListItems()
            => this.cars.All()
                .Select(c => new SelectListItem
                {
                    Text = $"{c.Make} - {c.Model}",
                    Value = c.Id.ToString()
                });
    }
}
