﻿namespace LearningSystem.Services.Models.Courses
{
    using LearningSystem.Common.Mapping;
    using LearningSystem.Data.Models;
    using System;

    public class CourseListingModel : IMapFrom<Course>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
