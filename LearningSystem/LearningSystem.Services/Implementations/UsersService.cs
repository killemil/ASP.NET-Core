namespace LearningSystem.Services.Implementations
{
    using AutoMapper.QueryableExtensions;
    using Data;
    using Models.Users;
    using System.Collections.Generic;
    using System.Linq;

    public class UsersService : IUsersService
    {
        private readonly LearningSystemDbContext db;

        public UsersService(LearningSystemDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<UserCourseModel> Courses(string studentId)
            => this.db.Users
            .Where(u => u.Id == studentId)
            .SelectMany(u=> u.Courses)
            .ProjectTo<UserCourseModel>(new { studentId })
            .ToList();

    }
}
