namespace LearningSystem.Services
{
    using LearningSystem.Services.Models.Admins;
    using System.Collections.Generic;

    public interface IAdminService
    {
        IEnumerable<AdminUserListingModel> UserListing();

        AdminUserDetailsModel GetUserById(string id);
    }
}
