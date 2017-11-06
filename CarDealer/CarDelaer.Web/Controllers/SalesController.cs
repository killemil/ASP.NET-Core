namespace CarDelaer.Web.Controllers
{
    using CarDealer.Services;
    using Microsoft.AspNetCore.Mvc;

    [Route("sales")]
    public class SalesController : Controller
    {
        private readonly ISaleService sales;

        public SalesController(ISaleService sales)
        {
            this.sales = sales;
        }

        public IActionResult All()
        {
            return View(this.sales.All(false, null));
        }

        [Route("{id}")]
        public IActionResult Details(int id)
        {
            return View(this.sales.ById(id));
        }

        [Route("discounted")]
        public IActionResult Discounted()
        {

            return View(this.sales.All(true, null));
        }

        [Route("discounted/{percent}")]
        public IActionResult Discounted(double percent)
        {
            return View(this.sales.All(true, percent));
        }
    }
}
