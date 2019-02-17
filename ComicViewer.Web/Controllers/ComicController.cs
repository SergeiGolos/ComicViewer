using System.Collections.Generic;
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

        [HttpGet("{search}")]
        public IEnumerable<ComicBookFile> Get(string search) => this.resolver.FindByName(search);
    }    
}
