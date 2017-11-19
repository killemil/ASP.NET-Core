namespace CameraBazaar.Web.Controllers
{
    using CameraBazaar.Data.Models;
    using CameraBazaar.Services;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    public class AdminController : Controller
    {
        private readonly IAdminService admins;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<User> userManager;

        public AdminController(
            IAdminService admins,
            RoleManager<IdentityRole> roleManager,
            UserManager<User> userManager)
        {
            this.admins = admins;
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        public IActionResult Users()
            => View(admins.AllUsers());

        public async Task<IActionResult> Details(string id)
        {
            var user = await this.userManager.FindByIdAsync(id);
            var currentUser = this.admins.UserById(id);

            if (user == null)
            {
                return NotFound();
            }

            var roles = await this.userManager.GetRolesAsync(user);
            currentUser.Roles = roles;

            return View(currentUser);
        }
    }
}
