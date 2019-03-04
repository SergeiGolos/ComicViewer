
namespace ComicViewer.Web.Controllers
{
    using System.IO;
    using Microsoft.AspNetCore.Mvc;

    using ComicViewer.Core;    

    [Route("api/[controller]")]
    public class ImageController : Controller
    {
        private readonly IComicBookResolver indexResolver;
        private readonly IComicBookFactory factory;
        private readonly IImageProcessor processor;

        public ImageController(IComicBookResolver indexResolver, IComicBookFactory factory, IImageProcessor processor)
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
            var image = factory.LoadPage(new FileInfo(book.Path), pageNumber);
            if (image == null) { return new StatusCodeResult(404); }
            if (height.HasValue && width.HasValue)
            {
                image = this.processor.Resize(image, height.Value, width.Value);
            }
            return File(image, "image/jpeg"); //TODO: get 
        }

        [HttpGet("thumb/{id}/{pageNumber}")]
        public IActionResult GetThumb(string id, int pageNumber, [FromQuery(Name = "size")] string size = "small")
        {
            var book = this.indexResolver.FindById(id);
            if (book == null) return null;
            var image = factory.LoadThumb(new FileInfo(book.Path), pageNumber, size);
            if (image == null) { return new StatusCodeResult(404); }
            return File(image, "image/jpeg"); //TODO: get 
        }
    }
}
