namespace News.Api.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Services;

    using static WebConstants;

    public class NewsController : BaseController
    {
        private readonly INewsService news;

        public NewsController(INewsService news)
        {
            this.news = news;
        }

        [HttpGet]
        public IActionResult Get()
            => this.Ok(this.news.ALl());

        [HttpPost]
        public IActionResult Post([FromBody] PostCreateNewsRequresModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest();
            }

            var result = this.news.Create(model.Title, model.Content);

            return this.Ok(result);
        }

        [HttpPut(WithId)]
        public IActionResult Edit(int id, [FromBody] PutEditNewsRequestModel model)
        {
            if (!ModelState.IsValid || !this.news.Exist(id))
            {
                return BadRequest();
            }

            var result = this.news.Edit(id, model.Title, model.Content, model.PublishedDate);

            return this.Ok(result);
        }

        [HttpDelete(WithId)]
        public IActionResult Delete(int id)
        {
            if (!this.news.Exist(id))
            {
                return BadRequest();
            }

            var message = this.news.Delete(id);

            return this.Ok(message);
        }
    }
}
