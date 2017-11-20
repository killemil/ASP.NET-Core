namespace LearningSystem.Web.Controllers
{
    using Infrastructure;
    using LearningSystem.Data.Models;
    using LearningSystem.Services;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Models.CourseVewModels;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class CoursesController : Controller
    {
        private readonly ICourseService courses;
        private readonly UserManager<User> userManager;

        public CoursesController(
            ICourseService courses,
            UserManager<User> userManager)
        {
            this.courses = courses;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Create()
        {
            return View(new CreateCourseViewModel
            {
                Trainers = await this.GetTreinersItems()
            });

        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateCourseViewModel model, string trainer)
        {
            if (!ModelState.IsValid)
            {
                model.Trainers = await this.GetTreinersItems();
                return View(model);
            }

            this.courses.Create(model.Name,
                model.Description,
                model.StartDate,
                model.EndDate,
                trainer);

            return Redirect("/");
        }

        public IActionResult Details(int id)
            => View(this.courses.ById(id));

        public IActionResult SignUp(int id, string username)
        {
            this.courses.SignUp(id, username);

            return RedirectToAction(nameof(Details), new { id = id });
        }

        //TODO Done remove user from course
        public IActionResult SignOut(int id, string username)
        {
            this.courses.SignOut(id, username);

            return RedirectToAction(nameof(Details), new { id = id });
        }

        private async Task<IEnumerable<SelectListItem>> GetTreinersItems()
        {
            var trainers = await this.userManager.GetUsersInRoleAsync(GlobalConstants.Trainer);
            return trainers
                    .Select(t => new SelectListItem
                    {
                        Text = t.UserName,
                        Value = t.Id
                    })
                    .ToList();
        }
    }
}
