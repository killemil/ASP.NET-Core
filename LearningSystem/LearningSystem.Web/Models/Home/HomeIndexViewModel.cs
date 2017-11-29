namespace LearningSystem.Web.Models.Home
{
    using LearningSystem.Services.Models.Courses;
    using System.Collections.Generic;

    public class HomeIndexViewModel : SearchFormModel
    {
        public IEnumerable<CourseListingModel> Courses { get; set; }
    }
}
