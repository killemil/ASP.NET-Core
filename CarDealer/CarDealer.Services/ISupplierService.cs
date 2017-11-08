namespace CarDealer.Services
{
    using CarDealer.Services.Models.Suppliers;
    using System.Collections.Generic;

    public interface ISupplierService
    {
        IEnumerable<SupplierListingModel> AllListing(bool isImporter);

        IEnumerable<SupplierModel> All();
    }
}
