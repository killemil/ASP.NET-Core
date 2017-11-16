namespace CameraBazaar.Services.Models.Users
{
    using CameraBazaar.Services.Models.Cameras;
    using System.Collections.Generic;
    public class UserDetailsModel
    {
        public string Username { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public IEnumerable<CameraListingModel> Cameras { get; set; }
    }
}
