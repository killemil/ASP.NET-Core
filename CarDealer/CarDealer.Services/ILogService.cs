namespace CarDealer.Services
{
    using CarDealer.Data.Models.Enums;
    using CarDealer.Services.Models.Logs;
    using System.Collections.Generic;

    public interface ILogService
    {
        IEnumerable<LogListingModel> AllListing(string searchTerm, int page = 1, int pageSize = 10);

        void ClearLogs();

        void Create(string username, string table, Operation operation);

        int Total(string searchTerm);
    }
}
