namespace News.Services.Models
{
    using Common.Mapping;
    using Data.Models;
    using System;

    public class NewsListingServiceModel : IMapFrom<News>
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime PublishDate { get; set; }
    }
}
