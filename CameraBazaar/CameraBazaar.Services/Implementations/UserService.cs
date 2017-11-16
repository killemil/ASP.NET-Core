namespace CameraBazaar.Services.Implementations
{
    using System;
    using CameraBazaar.Services.Models.Users;
    using CameraBazaar.Web.Data;
    using System.Linq;
    using CameraBazaar.Services.Models.Cameras;

    public class UserService : IUserService
    {
        private readonly CameraBazaarDbContext db;

        public UserService(CameraBazaarDbContext db)
        {
            this.db = db;
        }

        public UserDetailsModel ById(string id)
            => this.db.Users
                .Where(u => u.Id == id)
                .Select(u => new UserDetailsModel
                {
                    Username = u.UserName,
                    Email = u.Email,
                    Phone = u.PhoneNumber,
                    Cameras = u.Cameras.Select(c => new CameraListingModel
                    {
                        Id = c.Id,
                        Make = c.Make,
                        Model = c.Model,
                        Price = c.Price,
                        Quantity = c.Quantity,
                        ImageUrl = c.ImageUrl
                    })
                })
                .FirstOrDefault();
    }
}
