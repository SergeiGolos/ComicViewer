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

        [HttpGet("find/{search}")]
        public IEnumerable<ComicBookFile> Find(string search) => this.resolver.FindByName(search).Take(20);
    }    
}
