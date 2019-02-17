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
            comic.Id = file.GetHashCode().ToString("X");
            comic.Name = file.Name.Remove(file.Name.IndexOf(file.Extension));
            comic.Path = file.FullName;
            comic.Extension = file.Extension;
            comic.IsSolid = archive.IsSolid;
            comic.Created = DateTime.Now;
        }
    }
}
