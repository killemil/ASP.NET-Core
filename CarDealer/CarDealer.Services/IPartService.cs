namespace CarDealer.Services
{
    using CarDealer.Services.Models.Parts;
    using System.Collections.Generic;

    public interface IPartService
    {
        IEnumerable<PartListingModel> AllListing(int page = 1, int pageSize = 10);

        IEnumerable<PartSelectModel> All();

        void Create(string name, decimal price, int quantity, int supplierId);

        PartListingModel ById(int id);

        void Edit(int id, decimal price, int quantity);

        void Delete(int id);
        
        bool Exist(int id);

        int Total();
    }
}
