namespace LearningSystem.Web.Controllers
{
    using LearningSystem.Services;
    using LearningSystem.Web.Models;
    using LearningSystem.Web.Models.Home;
    using Microsoft.AspNetCore.Mvc;
    using System.Diagnostics;

    public class HomeController : Controller
    {
        private readonly ICourseService courses;
        private readonly IUsersService users;

        public HomeController(ICourseService courses, IUsersService users)
        {
            this.courses = courses;
            this.users = users;
        }

        public IActionResult Index()
        {
            return View(new HomeIndexViewModel
            {
                Courses = this.courses.AllListing()
            });
        }

        public IActionResult Search (SearchFormModel model)
        {
            var viewModel = new SearchListingModel
            {
                SeachText = model.SearchText
            };

            if (model.SearchInCourses)
            {
                viewModel.Courses = this.courses.Find(model.SearchText);
            }

            if (model.SearchInStudents)
            {
                viewModel.Users = this.users.Find(model.SearchText);
            }
            
            return View(viewModel);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
