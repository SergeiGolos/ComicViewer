namespace ComicViewer.Core
{
    using System.IO;

    public interface IComicBookFactory
    {
        ComicBookFile LoadFile(FileInfo file);
        MemoryStream LoadPage(FileInfo file, int pageIndex);
        MemoryStream LoadPage(FileInfo file, int pageIndex, int? height, int? width);
        MemoryStream LoadThumb(FileInfo fileInfo, int pageNumber, string size);
    }
}