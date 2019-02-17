namespace ComicViewer.Core
{
    using System.Collections.Generic;

    public interface IComicBookResolver
    {
        IEnumerable<ComicBookFile> FindByName(string search);
        ComicBookFile FindById(string id);
    }
}