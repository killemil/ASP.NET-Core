namespace LearningSystem.Web.Models.AdminViewModels
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;

    public class AddRoleViewModel
    {
        public string UserId { get; set; }

        public IEnumerable<SelectListItem> Roles { get; set; }
    }
}
