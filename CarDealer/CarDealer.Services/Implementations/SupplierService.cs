namespace CarDealer.Services.Implementations
{
    using System.Collections.Generic;
    using CarDealer.Data;
    using CarDealer.Services.Models.Suppliers;
    using System.Linq;

    public class SupplierService : ISupplierService
    {
        private readonly CarDealerDbContext db;

        public SupplierService(CarDealerDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<SupplierListingModel> AllListing(bool isImporter)
            => this.db.Suppliers
                .Where(s => s.IsImporter == isImporter)
                .Select(s => new SupplierListingModel
                {
                    Id = s.Id,
                    Name = s.Name,
                    TotalParts = s.Parts.Count
                })
                .ToList();

        public IEnumerable<SupplierModel> All()
            => this.db.Suppliers
                .OrderBy(c => c.Name)
                .Select(s => new SupplierModel
                {
                    Id = s.Id,
                    Name = s.Name
                })
                .ToList();
    }
}
