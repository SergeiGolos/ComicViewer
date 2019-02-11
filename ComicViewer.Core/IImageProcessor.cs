using System.IO;

namespace ComicViewer.Core
{
    public class DummyImageProcessor : IImageProcessor
    {
        public Stream Resize(Stream source, int height, int width)
        {
            return source;
        }
    }

    public interface IImageProcessor
    {
        Stream Resize(Stream source, int height, int width);
    }
}