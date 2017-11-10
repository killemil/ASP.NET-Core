namespace CarDealer.Services.Implementations
{
    using System.Collections.Generic;
    using CarDealer.Data;
    using CarDealer.Services.Models.Suppliers;
    using System.Linq;
    using CarDealer.Data.Models;

    public class SupplierService : ISupplierService
    {
        private readonly CarDealerDbContext db;

        public SupplierService(CarDealerDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<SupplierListingModel> AllListing()
            => this.db.Suppliers
                .OrderByDescending(c => c.Id)
                .Select(s => new SupplierListingModel
                {
                    Id = s.Id,
                    Name = s.Name,
                    IsImporter = s.IsImporter,
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

        public void Create(string name, bool isImporter)
        {
            var supplier = new Supplier
            {
                Name = name,
                IsImporter = isImporter
            };

            this.db.Suppliers.Add(supplier);
            this.db.SaveChanges();
        }

        public SupplierEditModel ById(int id)
            => this.db.Suppliers
                .Where(s => s.Id == id)
                .Select(s => new SupplierEditModel
                {
                    Name = s.Name,
                    IsImporter = s.IsImporter
                })
                .FirstOrDefault();

        public void Edit(int id, string name, bool isImporter)
        {
            var supplier = this.db.Suppliers.Find(id);

            if (supplier == null)
            {
                return;
            }

            supplier.Name = name;
            supplier.IsImporter = isImporter;

            this.db.SaveChanges();
        }

        public void Delete(int id)
        {
            var supplier = this.db.Suppliers
                .FirstOrDefault(s => s.Id == id);

            if (supplier == null)
            {
                return;
            }

            foreach (var part in supplier.Parts)
            {
                this.db.Parts.Remove(part);
            }

            this.db.Suppliers.Remove(supplier);
            this.db.SaveChanges();
        }
    }
}
