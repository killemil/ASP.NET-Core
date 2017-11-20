using System.Collections.Generic;

namespace LearningSystem.Services.Models.Courses
{
    public class CourseDetailsModel : CourseListingModel
    {
        public string TrainerId { get; set; }

        public string TrainerName { get; set; }

        public int NumberOfStudents { get; set; }

        public IEnumerable<string> StudentUsenames { get; set; }
    }
}
