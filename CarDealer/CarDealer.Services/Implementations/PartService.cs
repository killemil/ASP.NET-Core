namespace CarDealer.Services.Implementations
{
    using System.Collections.Generic;
    using CarDealer.Data;
    using CarDealer.Services.Models.Parts;
    using System.Linq;
    using CarDealer.Data.Models;

    public class PartService : IPartService
    {
        private readonly CarDealerDbContext db;

        public PartService(CarDealerDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<PartListingModel> AllListing(int page = 1, int pageSize = 10)
            => this.db.Parts
                .OrderByDescending(c => c.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(p => new PartListingModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    Quantity = p.Quantity,
                    SupplierName = p.Supplier.Name
                })
                .ToList();

        public IEnumerable<PartSelectModel> All()
            => this.db.Parts
                .OrderBy(p => p.Name)
                .Select(p => new PartSelectModel
                {
                    Id = p.Id,
                    Name = p.Name
                })
                .ToList();

        public void Create(string name, decimal price, int quantity, int supplierId)
        {
            var part = new Part
            {
                Name = name,
                Price = price,
                Quantity = quantity,
                SupplierId = supplierId
            };

            this.db.Parts.Add(part);
            this.db.SaveChanges();
        }

        public PartListingModel ById(int id)
            => this.db.Parts
                .Where(p => p.Id == id)
                .Select(p => new PartListingModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    Quantity = p.Quantity,
                    SupplierName = p.Supplier.Name
                })
                .FirstOrDefault();

        public void Delete(int id)
        {
            var part = this.db.Parts.Find(id);

            if (part == null)
            {
                return;
            }

            this.db.Parts.Remove(part);
            this.db.SaveChanges();
        }

        public void Edit(int id, decimal price, int quantity)
        {
            var part = this.db.Parts.Find(id);

            if (part == null)
            {
                return;
            }

            part.Price = price;
            part.Quantity = quantity;

            this.db.SaveChanges();
        }

        public bool Exist(int id)
            => this.db.Parts.Any(p => p.Id == id);

        public int Total()
            => this.db.Parts.Count();
    }
}
