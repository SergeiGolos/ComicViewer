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
            return this.Files
                .Where(file => file.Value.Name.IndexOf(search, StringComparison.CurrentCultureIgnoreCase) >= 0)
                .Select(n => n.Value);
        }        
    }
}