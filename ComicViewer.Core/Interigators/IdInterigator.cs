namespace ComicViewer.Core
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    using SharpCompress.Archives;

    public class IdInterigator : IComicInterigator
    {
        public void Apply(ComicBookFile comic, FileInfo file, IArchive archive, IEnumerable<IArchiveEntry> pages)
        {
            comic.Id = file.FullName.GetHashCode().ToString("X");            
            comic.Path = file.FullName;
            comic.OriginalName = file.Name.Remove(file.Name.IndexOf(file.Extension));
            comic.Extension = file.Extension;            
            comic.Created = DateTime.Now;
        }
    }
}
