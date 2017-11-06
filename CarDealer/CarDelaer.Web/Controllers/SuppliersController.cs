namespace CarDelaer.Web.Controllers 
{
    using CarDealer.Services;
    using CarDelaer.Web.Models.Suppliers;
    using Microsoft.AspNetCore.Mvc;

    public class SuppliersController : Controller
    {
        private const string SuppliersView = "Suppliers";

        private readonly ISupplierService suppliers;

        public SuppliersController(ISupplierService suppliers)
        {
            this.suppliers = suppliers;
        }

        public IActionResult Local()
        {
            return View(SuppliersView, this.GetSupplierModel(false));
        }

        public IActionResult Importers()
        {
            return View(SuppliersView, this.GetSupplierModel(true));
        }

        private SuppliersModel GetSupplierModel (bool isImporter)
        {
            var type = isImporter ? "Importer" : "Local";

            var suppliers = this.suppliers.All(isImporter);

            return new SuppliersModel
            {
                Type = type,
                Suppliers = suppliers
            };
        }
    }
}
