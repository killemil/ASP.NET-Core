namespace CarDelaer.Web.Controllers
{
    using CarDealer.Services;
    using CarDelaer.Web.Models.Cars;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;
    using System.Linq;

    [Route("cars")]
    public class CarsController : Controller
    {
        private readonly ICarService cars;
        private readonly IPartService parts;

        public CarsController(
            ICarService cars,
            IPartService parts)
        {
            this.cars = cars;
            this.parts = parts;
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
            => View(new CarFormModel
            {
                Parts = this.GetPartsListItems()
            });

        [HttpPost]
        [Route(nameof(Create))]
        public IActionResult Create(CarFormModel carModel)
        {
            if (!ModelState.IsValid)
            {
                carModel.Parts = this.GetPartsListItems();
                return View(carModel);
            }

            this.cars.Create(
                carModel.Make, 
                carModel.Model, 
                carModel.TravelledDistance,
                carModel.PartIds);

            return RedirectToAction(nameof(All));
        }

        private IEnumerable<SelectListItem> GetPartsListItems()
            => this.parts.All()
                .Select(p => new SelectListItem
                {
                    Text = p.Name,
                    Value = p.Id.ToString()
                });
    }
}
