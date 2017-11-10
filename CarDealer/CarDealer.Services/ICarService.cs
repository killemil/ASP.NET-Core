namespace CarDealer.Services
{
    using CarDealer.Services.Models.Cars;
    using System.Collections.Generic;

    public interface ICarService
    {
        IEnumerable<CarModel> AllListing();

        IEnumerable<CarModel> ByMake(string make);

        IEnumerable<CarWithPartsModel> CarWithParts();

        void Create(string make, string model, long travelledDistance, IEnumerable<int> parts);

        IEnumerable<CarSelectModel> All();

        CarPriceModel ById(int id);
    }
}
