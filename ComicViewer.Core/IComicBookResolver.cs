namespace ComicViewer.Core
{
    using System.Collections.Generic;

    public interface IComicBookResolver
    {
        IEnumerable<ComicBookFile> FindByName(string search);
        ComicBookFile FindById(string id);
        ComicBookFile FindByPath(string path);
        void Store(ComicBookFile file);
    }
}