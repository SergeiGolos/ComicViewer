namespace ComicViewer.Core
{
    using System.Collections.Generic;
    using ComicViewer.Core.Model;

    public interface IComicBookResolver
    {
        IEnumerable<ComicBookFile> Search(string[] terms);
        IEnumerable<ComicBookFile> FindPublishers();
        IEnumerable<ComicBookFile> FindByName(string name);
        IEnumerable<ComicBookFile> FindByPublisher(string publisher);
        IEnumerable<ComicPageFile> FindPagesById(string id);
        ComicBookFile FindById(string id);
        ComicBookFile FindByPath(string path);
        
        void Store(ComicBookFile file);
        
    }
}