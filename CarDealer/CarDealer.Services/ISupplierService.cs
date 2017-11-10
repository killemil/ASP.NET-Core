namespace CarDealer.Services
{
    using CarDealer.Services.Models.Suppliers;
    using System.Collections.Generic;

    public interface ISupplierService
    {
        IEnumerable<SupplierListingModel> AllListing();

        IEnumerable<SupplierModel> All();

        void Create(string name, bool isImporter);

        SupplierEditModel ById(int id);

        void Edit(int id, string name, bool isImporter);

        void Delete(int id);
    }
}
