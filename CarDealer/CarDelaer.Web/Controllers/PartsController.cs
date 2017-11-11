namespace CarDealer.Web.Controllers
{
    using CarDealer.Services;
    using CarDealer.Web.Models.Parts;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class PartsController : Controller
    {
        private const int PageSize = 25;

        private readonly IPartService parts;
        private readonly ISupplierService suppliers;

        public PartsController(
            IPartService parts,
            ISupplierService suppliers)
        {
            this.parts = parts;
            this.suppliers = suppliers;
        }

        public IActionResult All(int page = 1)
        {
            var parts = this.parts.AllListing(page);

            return View(new PartPageListingModel
            {
                Parts = this.parts.AllListing(page, PageSize),
                CurrentPage = page,
                TotalPages = (int)Math.Ceiling(this.parts.Total() / (double)PageSize)
            });
        }

        public IActionResult Create()
            => View(new PartFormModel
            {
                Suppliers = this.GetSuppliersListItems()
            });

        [HttpPost]
        public IActionResult Create(PartFormModel partModel)
        {
            if (!ModelState.IsValid)
            {
                partModel.Suppliers = this.GetSuppliersListItems();
                return View(partModel);
            }

            this.parts.Create(partModel.Name, partModel.Price, partModel.Quantity, partModel.SupplierId);

            return RedirectToAction(nameof(All));
        }

        public IActionResult Edit(int id)
        {
            var currentPart = this.parts.ById(id);

            if (currentPart == null)
            {
                return NotFound();
            }

            return View(new PartFormModel
            {
                Name = currentPart.Name,
                Price = currentPart.Price,
                Quantity = currentPart.Quantity,
                IsEdit = true
            });
        }

        [HttpPost]
        public IActionResult Edit(int id, PartFormModel partForm)
        {
            if (!ModelState.IsValid)
            {
                partForm.IsEdit = true;
                return View(partForm);
            }

            this.parts.Edit(
                    id,
                    partForm.Price,
                    partForm.Quantity);

            return RedirectToAction(nameof(All));
        }

        public IActionResult Delete(int id)
        {
            if (!this.parts.Exist(id))
            {
                return NotFound();
            }

            var currentPart = this.parts.ById(id);
            if (currentPart == null)
            {
                return NotFound();
            }

            return View(currentPart);
        }

        public IActionResult ConfirmDelete(int id)
        {
            if (!this.parts.Exist(id))
            {
                return NotFound();
            }

            this.parts.Delete(id);

            return RedirectToAction(nameof(All));
        }

        private IEnumerable<SelectListItem> GetSuppliersListItems()
            => this.suppliers.All()
                .Select(s => new SelectListItem
                {
                    Text = s.Name,
                    Value = s.Id.ToString()
                });
    }
}