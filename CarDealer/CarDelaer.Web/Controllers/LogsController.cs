namespace CarDealer.Web.Controllers
{
    using CarDealer.Services;
    using CarDealer.Web.Models.Logs;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Linq;

    public class LogsController : Controller
    {
        private const int PageSize = 10;

        private readonly ILogService logs;

        public LogsController(ILogService logs)
        {
            this.logs = logs;
        }

        public IActionResult All(string searchTerm, int page = 1)
        {
            return View(new LogPageListingModel
            {
                Logs = this.logs.AllListing(searchTerm, page, PageSize),
                CurrentPage = page,
                TotalPages = (int)Math.Ceiling(this.logs.Total(searchTerm) / (double)PageSize)
            });
        }

        public IActionResult Clear()
        {
            this.logs.ClearLogs();

            return RedirectToAction(nameof(All));
        }
    }
}
