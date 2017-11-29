namespace LearningSystem.Services.Models.Users
{
    using AutoMapper;
    using Common.Mapping;
    using Data.Models;

    public class UserListingModel : IMapFrom<User>, IHaveCustomMapping
    {
        public string Username { get; set; }

        public string Name { get; set; }

        public int  Courses { get; set; }

        public void ConfigureMapping(Profile mapper)
            => mapper
                .CreateMap<User, UserListingModel>()
                .ForMember(u => u.Courses, cfg => cfg.MapFrom(u => u.Courses.Count));
    }
}
