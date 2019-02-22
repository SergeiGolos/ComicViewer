namespace ComicViewer.Core
{
    using System.Collections.Generic;

    public interface IComicBookResolver
    {
        IEnumerable<ComicBookFile> Search(string[] terms);
        IEnumerable<ComicBookFile> FindPublishers();
        IEnumerable<ComicBookFile> FindByName(string name);
        IEnumerable<ComicBookFile> FindByPublisher(string publisher);
        ComicBookFile FindById(string id);
        ComicBookFile FindByPath(string path);
        
        void Store(ComicBookFile file);
    }
}