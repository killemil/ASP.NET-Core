namespace CarDealer.Services.Implementations
{
    using System.Collections.Generic;
    using CarDealer.Data;
    using CarDealer.Services.Models.Logs;
    using System.Linq;
    using CarDealer.Data.Models.Enums;
    using CarDealer.Data.Models;
    using System;

    public class LogService : ILogService
    {
        private readonly CarDealerDbContext db;

        public LogService(CarDealerDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<LogListingModel> AllListing(string searchTerm, int page = 1, int pageSize = 10)
        {
            var logs = this.db.Logs.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                logs = logs.Where(l => l.Username.ToLower() == searchTerm.ToLower());
            }

            return logs
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(l => new LogListingModel
                {
                    Username = l.Username,
                    Operation = l.Operation,
                    Table = l.Table,
                    Time = l.Modified
                })
                .ToList();
        }

        public void ClearLogs()
        {
            var allLogs = this.db.Logs.ToList();

            this.db.Logs.RemoveRange(allLogs);
            this.db.SaveChanges();
        }

        public void Create(string username, string table, Operation operation)
        {
            var log = new Log
            {
                Username = username,
                Table = table,
                Operation = operation,
                Modified = DateTime.UtcNow
            };

            this.db.Logs.Add(log);
            this.db.SaveChanges();
        }

        public int Total(string searchTerm)
        {
            var logs = this.db.Logs.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                logs = logs.Where(l => l.Username.ToLower() == searchTerm.ToLower());
            }

            return logs.Count();
        }
    }
}
