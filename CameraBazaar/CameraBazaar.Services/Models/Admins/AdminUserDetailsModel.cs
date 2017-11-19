namespace CameraBazaar.Services.Models.Admins
{
    using System.Collections.Generic;

    public class AdminUserDetailsModel : UserListingModel
    {
        public IEnumerable<string> Roles { get; set; }
    }
}
