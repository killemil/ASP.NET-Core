namespace LearningSystem.Web.Controllers
{
    using LearningSystem.Data.Models;
    using LearningSystem.Services;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class UsersController : Controller
    {
        private readonly IUsersService users;
        private readonly UserManager<User> userManager;

        public UsersController(IUsersService users, UserManager<User> userManager)
        {
            this.users = users;
            this.userManager = userManager;
        }

        public IActionResult Courses()
        {
            var userId = this.userManager.GetUserId(User);

            var courses = this.users.Courses(userId);

            return View();
        }
    }
}
