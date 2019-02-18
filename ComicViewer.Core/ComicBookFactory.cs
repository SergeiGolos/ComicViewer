namespace ComicViewer.Core
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    using SharpCompress.Archives;

    public class ComicBookFactory : IComicBookFactory
    {
        private readonly IEnumerable<IComicInterigator> interigators;
        private readonly IImageProcessor processor;
        private string[] imageExtentions = new[] { ".jpg", ".png", ".jpeg" };

        public ComicBookFactory(IImageProcessor processor, IEnumerable<IComicInterigator> interigators) {
            this.interigators = interigators;
            this.processor = processor;
        }        

        public T InArchive<T>(FileInfo file, Func<IArchive, IEnumerable<IArchiveEntry>, T> loaderFn)
        {        
            using (var archive = ArchiveFactory.Open(file.FullName))
            {
                var pages = archive.Entries.Where(m =>
                {
                    var extensionIndex = m.Key.LastIndexOf(".");
                    if (extensionIndex == -1) return false;

                    var extension = m.Key.Substring(m.Key.LastIndexOf(".")).ToLower();

                    return imageExtentions.Contains(extension);
                }).OrderBy(m => m.Key);

                return loaderFn(archive, pages);
            }
        }

        public ComicBookFile LoadFile(FileInfo file)
        {
            return InArchive(file, (archive, pages) => {
                ComicBookFile comic = null;
                try
                {
                    if (archive.IsSolid) { return null; } // TODO add processing to solid files.

                    comic = new ComicBookFile();
                    foreach (var interigator in this.interigators)
                    {
                        interigator.Apply(comic, file, archive, pages);
                    }
                }
                catch { }
                return comic;
            });            
        } 
        
        public MemoryStream LoadPage(FileInfo file, int pageIndex)
        {            
            return InArchive(file, (archive, pages) =>
            {
                var stream = new MemoryStream();
                if (!archive.IsSolid) { 
                    pages.ElementAt(pageIndex - 1)
                        .OpenEntryStream()
                        .CopyTo(stream);
                }
                stream.Seek(0, SeekOrigin.Begin);
                return stream;
            });
        }

        public MemoryStream LoadPage(FileInfo file, int pageIndex, int? height, int? width)
        {
            var image = LoadPage(file, pageIndex);
            if (!height.HasValue && !width.HasValue)
            {
                return image;                                
            }

            using (image)
                return this.processor.Resize(image, height.Value, width.Value);

        }
    }
}
