using System;
using System.Collections.Generic;
using System.IO;
using SharpCompress.Archives;

namespace ComicViewer.Core.Interigators
{
    public class TitleInterigator : IComicInterigator
    {
        public void Apply(ComicBookFile comic, FileInfo file, IArchive archive, IEnumerable<IArchiveEntry> pages)
        {
            var parts = comic.Name.Split(new[] { " - " }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length > 1)
            {
                comic.Name = parts[0];
                comic.Title = parts[1];
            }
        }
    }
}
