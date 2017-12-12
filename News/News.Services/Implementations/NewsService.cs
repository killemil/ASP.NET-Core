namespace News.Services.Implementations
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Data;
    using Data.Models;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class NewsService : INewsService
    {
        private readonly NewsDbContext db;
        private readonly IMapper mapper;

        public NewsService(NewsDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public IEnumerable<NewsListingServiceModel> ALl()
            => this.db.News
                .OrderByDescending(n => n.PublishDate)
                .ProjectTo<NewsListingServiceModel>()
                .ToList();

        public NewsListingServiceModel Create(string title, string content)
        {
            var news = new News
            {
                Title = title,
                Content = content,
                PublishDate = DateTime.UtcNow
            };

            this.db.News.Add(news);
            this.db.SaveChanges();

            return this.mapper.Map<NewsListingServiceModel>(news);
        }

        public NewsListingServiceModel Edit(int id, string title, string content, DateTime publishedDate)
        {
            var news = this.db.News.Find(id);

            if (news == null)
            {
                return null;
            }

            news.Title = title;
            news.Content = content;
            news.PublishDate = publishedDate;

            this.db.SaveChanges();

            return this.mapper.Map<NewsListingServiceModel>(news);
        }

        public bool Exist(int id)
            => this.db.News.Any(n => n.Id == id);

        public string Delete(int id)
        {
            var news = this.db.News.Find(id);

            this.db.News.Remove(news);
            this.db.SaveChanges();

            return $"News with id {id} was successfully deleted";
        }
    }
}
