namespace LearningSystem.Services.Implementations
{
    using LearningSystem.Data;
    using LearningSystem.Data.Models;
    using LearningSystem.Services.Models.Courses;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class CourseService : ICourseService
    {
        private readonly LearningSystemDbContext db;

        public CourseService(LearningSystemDbContext db)
        {
            this.db = db;
        }

        public void Create(string name, string description, DateTime startDate, DateTime endDate, string trainerId)
        {
            var trainer = this.db.Users.FirstOrDefault(u => u.Id == trainerId);

            if (trainer == null)
            {
                return;
            }

            var course = new Course
            {
                Name = name,
                Description = description,
                StartDate = startDate,
                EndDate = endDate,
                Trainer = trainer
            };

            this.db.Courses.Add(course);
            this.db.SaveChanges();
        }

        public IEnumerable<CourseListingModel> AllListing()
            => this.db.Courses
                .Select(c => new CourseListingModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                    StartDate = c.StartDate,
                    EndDate = c.EndDate
                })
                .ToList();

        public CourseDetailsModel ById(int id)
            => this.db.Courses
                .Where(c => c.Id == id)
                .Select(c => new CourseDetailsModel
                {
                    Id = c.Id,
                    Description = c.Description,
                    Name = c.Name,
                    StartDate = c.StartDate,
                    EndDate = c.EndDate,
                    NumberOfStudents = c.Students.Count,
                    StudentUsenames = c.Students.Select(s => s.User.UserName),
                    TrainerId = c.TrainerId,
                    TrainerName = c.Trainer.Name
                })
                .FirstOrDefault();

        public void SignUp(int courseId, string username)
        {
            var user = this.db.Users.FirstOrDefault(u => u.UserName == username);
            var course = this.db.Courses.Find(courseId);

            if (user == null || course == null)
            {
                return;
            }

            course.Students.Add(new UserCourse
            {
                User = user,
                Course = course
            });
            this.db.SaveChanges();
        }

        public void SignOut(int courseId, string username)
        {
            var user = this.db.Users.Include(u => u.Courses).FirstOrDefault(u => u.UserName == username);
            var course = this.db.Courses.Include(c => c.Students).FirstOrDefault(c => c.Id == courseId);

            if (user == null || course == null)
            {
                return;
            }

            course.Students.Remove(new UserCourse
            {
                User = user,
                Course = course
            });

            this.db.SaveChanges();
        }
    }
}
