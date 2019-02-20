using System.Collections.Generic;
using System.IO;
using SharpCompress.Archives;

namespace ComicViewer.Core.Interigators
{
    public class NameInterigator : IComicInterigator
    {
        public void Apply(ComicBookFile comic, FileInfo file, IArchive archive, IEnumerable<IArchiveEntry> pages)
        {
            comic.Name = comic.OriginalName;

        }
    }
}
