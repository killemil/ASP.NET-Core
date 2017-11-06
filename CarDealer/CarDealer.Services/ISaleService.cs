namespace CarDealer.Services
{
    using CarDealer.Services.Models.Sales;
    using System.Collections.Generic;

    public interface ISaleService
    {
        IEnumerable<SaleModel> All(bool hasDiscount, double? discountPercent);

        SaleByIdModel ById(int id);
    }
}
