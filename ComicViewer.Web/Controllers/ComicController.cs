using System.Collections.Generic;
using ComicViewer.Core;
using Microsoft.AspNetCore.Mvc;

namespace ComicViewer.Web.Controllers
{
    [Route("api/[controller]")]
    public class ComicController : Controller
    {
        private readonly IComicBookIndexResolver resolver;

        public ComicController(IComicBookIndexResolver resolver)
        {
            this.resolver = resolver;
        }

        [HttpGet("{search}")]
        public IEnumerable<ComicBookFileInfo> Get(string search) => this.resolver.FindByName(search);
    }    
}
