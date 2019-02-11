using SharpCompress.Archives;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ComicViewer.Core
{
    public class ComicBookFactory : IComicBookFactory
    {
        private string[] imageExtentions = new[] { "jpg", "png" };

        public T InArchive<T>(FileInfo file, Func<IArchive, IEnumerable<IArchiveEntry>, T> loaderFn)
        {        
            using (var archive = ArchiveFactory.Open(file.FullName))
            {
                var pages = archive.Entries.Where(m => imageExtentions.Contains(m.Key.Substring(m.Key.Length - 3)))
                    .OrderBy(m => m.Key);
                return loaderFn(archive, pages);
            }
        }

        public ComicBookFile LoadFile(FileInfo file)
        {
            return InArchive(file, (archive, pages) => new ComicBookFile()
            {
                Name = file.Name,
                Path = file.FullName,
                NumberOfPages = pages.Count()                
            });            
        } 
        
        public Stream LoadPage(FileInfo file, int pageIndex)
        {            
            return InArchive(file, (archive, pages) =>
            {
                if (archive.IsSolid) { return null; }

                var stream = new MemoryStream();
                pages.ElementAt(pageIndex - 1)
                    .OpenEntryStream()
                    .CopyTo(stream);

                stream.Seek(0, SeekOrigin.Begin);
                return stream;
            });
        }
    }
}
