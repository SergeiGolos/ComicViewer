using System;
using System.Threading.Tasks;
using ComicViewer.Core;
using Microsoft.AspNetCore.Mvc;

namespace ComicViewer.Web.Controllers
{
    [Route("api/[controller]")]
    public class ImageController : Controller
    {
        private readonly IComicBookIndexResolver indexResolver;
        private readonly IComicBookFactory factory;
        private readonly IImageProcessor processor;

        public ImageController(IComicBookIndexResolver indexResolver, IComicBookFactory factory, IImageProcessor processor)
        {
            this.indexResolver = indexResolver;
            this.factory = factory;
            this.processor = processor;
        }

        [HttpGet("{id}/{pageNumber}")]
        public IActionResult Get(string id, int pageNumber, [FromQuery(Name = "height")] int? height, [FromQuery(Name = "width")] int? width)
        {
            var book = this.indexResolver.FindById(id);
            if (book == null) return null;
            var image = factory.LoadPage(book.GetFileInfo(), pageNumber);
            if (image == null) { return new StatusCodeResult(404); }
            if (height.HasValue && width.HasValue)
            {
                image = this.processor.Resize(image, height.Value, width.Value);
            }
            return File(image, "image/jpeg"); //TODO: get 
        }
    }
}
