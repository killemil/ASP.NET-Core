namespace LearningSystem.Services
{
    using Models.Courses;
    using System;
    using System.Collections.Generic;

    public interface ICourseService
    {
        void Create(string name, string description, DateTime startDate, DateTime endDate, string trainerId);

        IEnumerable<CourseListingModel> AllListing();

        CourseDetailsModel ById(int id);

        void SignUp(int courseId, string username);

        void SignOut(int courseId, string username);
    }
}
