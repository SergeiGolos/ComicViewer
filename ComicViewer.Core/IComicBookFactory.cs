using System.IO;

namespace ComicViewer.Core
{

    public interface IComicBookFactory
    {
        ComicBookFile LoadFile(FileInfo file);
        Stream LoadPage(FileInfo file, int pageIndex);
    }
}