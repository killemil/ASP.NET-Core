using System.Collections.Generic;
using LearningSystem.Data;
using LearningSystem.Services.Models.Admins;
using System.Linq;

namespace LearningSystem.Services.Implementations
{
    public class AdminService : IAdminService
    {
        private readonly LearningSystemDbContext db;

        public AdminService(LearningSystemDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<AdminUserListingModel> UserListing()
           => this.db.Users
                .Select(u => new AdminUserListingModel
                {
                    Id = u.Id,
                    Email = u.Email,
                    Name = u.Name,
                    Username = u.UserName
                })
                .ToList();

        public AdminUserDetailsModel GetUserById(string id)
            => this.db.Users.Where(u => u.Id == id)
                .Select(u => new AdminUserDetailsModel
                {
                    Id = u.Id,
                    Name = u.Name,
                    Email = u.Email,
                    BirthDate = u.BirthDate,
                    Username = u.UserName
                })
                .FirstOrDefault();
    }
}
