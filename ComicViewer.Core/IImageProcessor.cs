using ComicViewer.Core.Configuration;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.Primitives;
using System.IO;

namespace ComicViewer.Core
{
    public class ImageSharpProcessor : IImageProcessor
    {
        private readonly ComicViewerConfiguration config;

        public ImageSharpProcessor(ComicViewerConfiguration config)
        {
            this.config = config;
        }        

        public MemoryStream Resize(MemoryStream image, int height, int width)
        {
            using (var img = Image.Load(image))
            {
                return this.Resize(img, height, width);
            }
        }

        public MemoryStream Resize(MemoryStream image, string size)
        {
            using (var img = Image.Load(image))
            {
                return this.Resize(img, size);
            }
        }

        public MemoryStream Resize(Image<Rgba32> image, int height, int width)
        {            
            var ms = new MemoryStream();
            
            image.Mutate(x => x.Resize(new Size(width, height)));
            image.Save(ms, new JpegEncoder());

            return ms;
        }        

        public MemoryStream Resize(Image<Rgba32> image, string size)
        {
            var currentSize = image.Size();
            var thumbHeight = config.ThumbnailHeight.ContainsKey(size) ? config.ThumbnailHeight[size] : config.ThumbnailHeight["small"];
            var newSize = new Size(currentSize.Width * (thumbHeight / currentSize.Height), thumbHeight);

            return this.Resize(image, newSize.Height, newSize.Width);
        }
    }

    public interface IImageProcessor
    {
        MemoryStream Resize(MemoryStream image, string size);
        MemoryStream Resize(MemoryStream image, int height, int width);

        MemoryStream Resize(Image<Rgba32> image, string size);
        MemoryStream Resize(Image<Rgba32> image, int height, int width);

    }
}