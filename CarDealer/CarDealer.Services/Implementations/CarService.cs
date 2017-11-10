namespace CarDealer.Services.Implementations
{
    using System.Collections.Generic;
    using CarDealer.Services.Models.Cars;
    using CarDealer.Data;
    using System.Linq;
    using CarDealer.Services.Models.Parts;
    using CarDealer.Data.Models;

    public class CarService : ICarService
    {
        private readonly CarDealerDbContext db;

        public CarService(CarDealerDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<CarModel> ByMake(string make)
            => this.db.Cars
                .Where(c => c.Make.ToLower() == make.ToLower())
                .OrderBy(c => c.Model)
                .ThenByDescending(c => c.TravelledDistance)
                .Select(c => new CarModel
                {
                    Make = c.Make,
                    Model = c.Model,
                    TravelledDistance = c.TravelledDistance
                })
                .ToList();

        public IEnumerable<CarWithPartsModel> CarWithParts()
            => this.db.Cars
                .OrderByDescending(c => c.Id)
                .Select(c => new CarWithPartsModel
                {
                    Make = c.Make,
                    Model = c.Model,
                    TravelledDistance = c.TravelledDistance,
                    Parts = c.Parts.Select(p => new PartModel
                    {
                        Name = p.Part.Name,
                        Price = p.Part.Price
                    })
                })
                .ToList();

        public IEnumerable<CarModel> AllListing()
            => this.db.Cars
                .OrderByDescending(c => c.Id)
                .Select(c => new CarModel
                {
                    Make = c.Make,
                    Model = c.Model,
                    TravelledDistance = c.TravelledDistance
                })
                .ToList();

        public void Create(string make, string model, long travelledDistance, IEnumerable<int> parts)
        {
            var existingParts = this.db.Parts
                .Where(p => parts.Contains(p.Id))
                .Select(p => p.Id)
                .ToList();

            var car = new Car
            {
                Make = make,
                Model = model,
                TravelledDistance = travelledDistance
            };

            foreach (var partId in existingParts)
            {
                car.Parts.Add(new PartCar
                {
                    PartId = partId
                });
            }


            this.db.Cars.Add(car);
            this.db.SaveChanges();
        }

        public IEnumerable<CarSelectModel> All()
            => this.db.Cars
                .OrderBy(c => c.Make)
                .ThenBy(c => c.Model)
                .Select(c => new CarSelectModel
                {
                    Id = c.Id,
                    Make = c.Make,
                    Model = c.Model
                });

        public CarPriceModel ById(int id)
            => this.db.Cars
                  .Where(c => c.Id == id)
                  .Select(c => new CarPriceModel
                  {
                      CarName = $"{c.Make} {c.Model}",
                      Price = c.Parts.Sum(p => p.Part.Price)
                  })
                  .FirstOrDefault();
    }
}
