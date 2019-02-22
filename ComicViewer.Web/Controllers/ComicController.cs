using System.Collections.Generic;
using System.Linq;
using ComicViewer.Core;
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
        public ComicBookFile Get(string id) => this.resolver.FindById(id);

        [HttpGet("publishers")]
        public IEnumerable<ComicBookFile> GetPublishers(string search) => this.resolver.FindPublishers();

        [HttpGet("publisher/{name}")]
        public IEnumerable<ComicBookFile> GetPublisher(string name, [FromQuery(Name = "skip")] int skip, [FromQuery(Name = "take")] int take) => this.resolver.FindByPublisher(name).Skip(skip).Take(take);

        [HttpGet("find/{search}")]
        public IEnumerable<ComicBookFile> Find(string search, [FromQuery(Name = "skip")] int skip, [FromQuery(Name = "take")] int take) => this.resolver.FindByName(search).Skip(skip).Take(take);
    }    
}
