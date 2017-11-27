namespace LearningSystem.Services
{
    using LearningSystem.Services.Models.Users;
    using System.Collections.Generic;

    public interface IUsersService
    {
        IEnumerable<UserCourseModel> Courses(string studentId);
    }
}
