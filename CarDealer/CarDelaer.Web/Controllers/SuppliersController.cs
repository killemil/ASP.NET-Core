namespace CarDealer.Web.Controllers
{
    using CarDealer.Services;
    using CarDealer.Web.Models.Suppliers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class SuppliersController : Controller
    {
        private readonly ISupplierService suppliers;

        public SuppliersController(ISupplierService suppliers)
        {
            this.suppliers = suppliers;
        }

        public IActionResult All()
        {
            var suppliers = this.suppliers.AllListing();

            return View(suppliers);
        }

        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Create(SupplierFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            this.suppliers.Create(model.Name, model.IsImporter);

            return RedirectToAction(nameof(All));
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            var supplier = this.suppliers.ById(id);
            if (supplier == null)
            {
                return NotFound();
            }

            return View(new SupplierFormModel
            {
                Name = supplier.Name,
                IsImporter = supplier.IsImporter,
                Id = id
            });
        }

        [Authorize]
        [HttpPost]
        public IActionResult Edit(SupplierFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            this.suppliers.Edit((int)model.Id, model.Name, model.IsImporter);

            return RedirectToAction(nameof(All));
        }

        [Authorize]
        public IActionResult Delete(int id)
        {
            var supplier = this.suppliers.ById(id);

            if (supplier == null)
            {
                return NotFound();    
            }

            return View(new SupplierFormModel
            {
                Id = id,
                IsImporter = supplier.IsImporter,
                Name = supplier.Name
            });
        }

        [Authorize]
        public IActionResult ConfirmDelete(int id)
        {
            this.suppliers.Delete(id);

            return RedirectToAction(nameof(All));
        }
    }
}
