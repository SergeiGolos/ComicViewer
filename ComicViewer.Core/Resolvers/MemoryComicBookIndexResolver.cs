namespace ComicViewer.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using ComicViewer.Core.Indexers;

    public class MemoryComicBookResolver : IComicBookResolver
    {
        private readonly InMemoryIndexer indexer;

        public MemoryComicBookResolver(InMemoryIndexer indexer)
        {            
            this.indexer = indexer;
        }

        public Dictionary<string, ComicBookFile> Files => indexer.Files;

        public ComicBookFile FindById(string id)
        {
            return this.Files.ContainsKey(id)
                ? this.Files[id]
                : null;
        }

        public IEnumerable<ComicBookFile> FindByName(string search)
        {
            return this.Files.Values
                .Where(file => file.Name.IndexOf(search, StringComparison.CurrentCultureIgnoreCase) >= 0);
        }

        public ComicBookFile FindByPath(string path)
        {
            return this.Files.Values
                .FirstOrDefault(file => file.Path == path);
        }

        public IEnumerable<ComicBookFile> FindByPublisher(string publisher)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ComicBookFile> FindPublishers()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ComicBookFile> Search(string[] terms)
        {
            throw new NotImplementedException();
        }

        public void Store(ComicBookFile file)
        {
            this.Files.Add(file.Id, file);            
        }
    }
}