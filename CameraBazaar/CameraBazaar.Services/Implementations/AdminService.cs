namespace CameraBazaar.Services.Implementations
{
    using CameraBazaar.Web.Data;
    using Models.Admins;
    using System.Collections.Generic;
    using System.Linq;

    public class AdminService : IAdminService
    {
        private readonly CameraBazaarDbContext db;

        public AdminService(CameraBazaarDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<UserListingModel> AllUsers()
            => this.db.Users
                .Select(u => new UserListingModel
                {
                    Id = u.Id,
                    Email = u.Email,
                    Username = u.UserName
                })
                .ToList();

        public AdminUserDetailsModel UserById(string id)
            => this.db.Users
                .Where(u => u.Id == id)
                .Select(u => new AdminUserDetailsModel
                {
                    Id = u.Id,
                    Email = u.Email,
                    Username = u.UserName
                })
                .FirstOrDefault();
    }
}
