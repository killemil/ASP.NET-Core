namespace News.Services
{
    using Models;
    using System;
    using System.Collections.Generic;

    public interface INewsService
    {
        IEnumerable<NewsListingServiceModel> ALl();

        NewsListingServiceModel Create(string title, string content);

        NewsListingServiceModel Edit(int id, string title, string content, DateTime publishedDate);

        bool Exist(int id);

        string Delete(int id);
    }
}
