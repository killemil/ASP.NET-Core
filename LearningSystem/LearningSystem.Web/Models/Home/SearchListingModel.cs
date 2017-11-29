namespace LearningSystem.Web.Models.Home
{
    using Services.Models.Courses;
    using Services.Models.Users;
    using System.Collections.Generic;

    public class SearchListingModel
    {
        public IEnumerable<CourseListingModel> Courses { get; set; } = new List<CourseListingModel>();

        public IEnumerable<UserListingModel> Users { get; set; } = new List<UserListingModel>();

        public string SeachText { get; set; }
    }
}
