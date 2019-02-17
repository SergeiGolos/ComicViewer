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
        public MemoryStream Resize(MemoryStream image, int height, int width)
        {
            using (var img = Image.Load(image))
            {
                return this.Resize(img, height, width);
            }
        }

        public MemoryStream Resize(Image<Rgba32> image, int height, int width)
        {
            var size = new Size(width, height);            
            var ms = new MemoryStream();
            
            image.Mutate(x => x.Resize(size));
            image.Save(ms, new JpegEncoder());

            return ms;
        }
    }

    public interface IImageProcessor
    {
        MemoryStream Resize(MemoryStream image, int height, int width);
        MemoryStream Resize(Image<Rgba32> image, int height, int width);
    }
}