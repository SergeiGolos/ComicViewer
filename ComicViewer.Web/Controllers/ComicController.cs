using System.Collections.Generic;
using System.Linq;
using ComicViewer.Core;
using ComicViewer.Core.Model;
using Microsoft.AspNetCore.Mvc;

namespace ComicViewer.Web.Controllers
{
    [Route("api/[controller]")]
    public class ComicController : Controller
    {
        private readonly IComicBookResolver resolver;

        public ComicController(IComicBookResolver resolver)
        {
            this.resolver = resolver;
        }
        [HttpGet("{id}")]
        public ComicBookResult Get(string id) => new ComicBookResult()
        {
            Comic = this.resolver.FindById(id),
            Pages = this.resolver.FindPagesById(id).Select(n => new ComicPageResult()
            {
                Page = n.Page,
                Width = n.Width,
                Height = n.Height,
                //`api/Image/${page.comicId}/${page.page}`;
                Url = $"api/Image/{n.ComicId}/{n.Page}"
            })
        };


        [HttpGet("publishers")]
        public IEnumerable<ComicBookFile> GetPublishers(string search) => this.resolver.FindPublishers();

        [HttpGet("publisher/{name}")]
        public IEnumerable<ComicBookFile> GetPublisher(string name, [FromQuery(Name = "skip")] int skip, [FromQuery(Name = "take")] int take) => this.resolver.FindByPublisher(name).Skip(skip).Take(take);

        [HttpGet("find/{search}")]
        public IEnumerable<ComicBookFile> Find(string search, [FromQuery(Name = "skip")] int skip, [FromQuery(Name = "take")] int take) {
        
            var test = this.resolver.FindByName(search).Skip(skip).Take(take);
            return test;
        }
    }    

    public class ComicPageResult
    {
        public int Page { get; set; }
        public string Url { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
    }

    public class ComicBookResult
    {
        public ComicBookFile Comic { get; set; }
        public IEnumerable<ComicPageResult> Pages { get; set; }
    }
}

