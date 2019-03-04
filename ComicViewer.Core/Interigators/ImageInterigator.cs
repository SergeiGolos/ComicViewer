namespace ComicViewer.Core
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using ComicViewer.Core.Configuration;
    using ComicViewer.Core.Model;
    using SharpCompress.Archives;
    using SixLabors.ImageSharp;
    using SixLabors.ImageSharp.Formats.Jpeg;
    using SixLabors.Primitives;

    public class ImageInterigator : IComicInterigator
    {
        private readonly ComicViewerConfiguration config;
        private readonly ComicBookContext context;
        private readonly IImageProcessor processor;        

        public ImageInterigator(ComicViewerConfiguration config, ComicBookContext context,  IImageProcessor processor)
        {
            this.config = config;
            this.context = context;
            this.processor = processor;            
        }

        private string getThumbBase(ComicBookFile comic)
        {
            var rootDirectoryparts = config.ComicRepositoryPath.Split(new[] { Path.DirectorySeparatorChar });
            var parts = comic.Path.Split(new[] { '.' }).ToList();
            var directory = string.Join(".", parts.Take(parts.Count() - 1));
            var directoryParts = directory.Split(new[] { Path.DirectorySeparatorChar });
            var thumbNameDirectoryParts = directoryParts.Take(rootDirectoryparts.Length)
                .Concat(new[] { "_thumbs_" })
                .Concat(directoryParts.Skip(rootDirectoryparts.Length));

            return string.Join(Path.DirectorySeparatorChar.ToString(), thumbNameDirectoryParts);
        }

        public void Apply(ComicBookFile comic, FileInfo file, IArchive archive, IEnumerable<IArchiveEntry> pages)
        {
            var index = 0;
            var thumbDirectory = getThumbBase(comic);

            Console.WriteLine("Generating Thumbnails in: {0}", thumbDirectory);
            foreach (var page in pages)
            {            
                using (var imgStream = page.OpenEntryStream())
                using (var image = Image.Load(imgStream))
                {
                    var pageFile = GetPagefile(comic, thumbDirectory, index, image.Size());                    
                    GenerateThumbnails(pageFile, image);
                    this.context.Pages.Add(pageFile);
                    index++;
                    
                }
                Console.Write(".");
            }
            Console.Write("\n");

            comic.Length = index;
        }

        private ComicPageFile GetPagefile(ComicBookFile comic, string thumbDirectory, int index, Size imageSize)
        {
            
            var fileName = string.Format("{0}-{1}.{{0}}.{2}", comic.Id, index, "jpg");            

            return new ComicPageFile()
            {
                ComicId = comic.Id,
                Page = index,
                FileNameMask = Path.Combine(thumbDirectory, fileName),
                Height = imageSize.Height,
                Width = imageSize.Width
            };
        }

        private void GenerateThumbnails(ComicPageFile pageFile, Image<SixLabors.ImageSharp.PixelFormats.Rgba32> image)
        {
            foreach (var size in config.ThumbnailHeight.Keys)
            {
                using (var thumbStream = this.processor.Resize(image, size))
                {
                    var filename = new FileInfo(string.Format(pageFile.FileNameMask, size));
                    
                    if (!filename.Directory.Exists) { filename.Directory.Create(); }
                    if (filename.Exists) { filename.Delete(); }

                    using (var thumb = File.OpenWrite(filename.FullName))
                    {
                        Image.Load(thumbStream, new JpegDecoder()).Save(thumb, new JpegEncoder());
                    }
                }
            }
        }
    }
}
