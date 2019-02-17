namespace ComicViewer.Core
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using ComicViewer.Core.Configuration;
    using SharpCompress.Archives;
    using SixLabors.ImageSharp;
    using SixLabors.Primitives;

    public class ImageInterigator : IComicInterigator
    {
        private readonly ComicViewerConfiguration config;
        private readonly IImageProcessor processor;
        private readonly Size size;

        public ImageInterigator(ComicViewerConfiguration config, IImageProcessor processor)
        {
            this.config = config;
            this.processor = processor;
            this.size = new Size(config.ThumbnailWidth.Value, config.ThumbnailHeight.Value);
        }

        public void Apply(ComicBookFile comic, FileInfo file, IArchive archive, IEnumerable<IArchiveEntry> pages)
        {
            using (var imgStream = pages.First().OpenEntryStream())
            using (var image = Image.Load(imgStream))
            {
                comic.Height = image.Height;
                comic.Width = image.Width;
                using (var thumbStream = this.processor.Resize(image, size.Height, size.Width))
                {
                    comic.Thumbnail = thumbStream.ToArray();
                }
            }

            comic.Length = pages.Count();
        }
    }
}
