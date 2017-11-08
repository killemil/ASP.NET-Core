namespace CarDelaer.Web.Controllers
{
    using CarDealer.Services;
    using CarDelaer.Web.Models.Cars;
    using Microsoft.AspNetCore.Mvc;

    [Route("cars")]
    public class CarsController : Controller
    {
        private readonly ICarService cars;

        public CarsController(ICarService cars)
        {
            this.cars = cars;
        }

        [Route("all")]
        public IActionResult All()
        {
            return View(this.cars.All());
        }

        [Route("{make}")]
        public IActionResult ByMake(string make)
        {
            var allCars = this.cars.ByMake(make);

            return View(new CarsByMakeModel
            {
                Make = make,
                Cars = allCars
            });
        }
        
        [Route("parts")]
        public IActionResult Parts()
        {
            return View(this.cars.CarWithParts());
        }

        [Route(nameof(Create))]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Route(nameof(Create))]
        public IActionResult Create(CarFormModel carModel)
        {
            if (!ModelState.IsValid)
            {
                return View(carModel);
            }

            this.cars.Create(
                carModel.Make, 
                carModel.Model, 
                carModel.TravelledDistance);

            return RedirectToAction(nameof(All));
        }
    }
}
