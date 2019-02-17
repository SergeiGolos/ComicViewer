namespace ComicViewer.Core
{
    using System.Collections.Generic;
    using System.IO;

    using SharpCompress.Archives;

    public interface IComicInterigator
    {
        void Apply(ComicBookFile comic, FileInfo file, IArchive archive, IEnumerable<IArchiveEntry> pages);
    }
}
