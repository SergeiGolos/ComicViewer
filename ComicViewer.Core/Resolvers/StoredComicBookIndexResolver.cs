namespace ComicViewer.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using ComicViewer.Core.Configuration;

    public class StoreComicBookResolver : IComicBookResolver
    {
        private readonly ComicViewerConfiguration config;
        private readonly ComicBookContext context;

        public StoreComicBookResolver(ComicViewerConfiguration config, ComicBookContext context)
        {
            this.config = config;
            this.context = context;
        }

        public ComicBookFile FindById(string id)
        {
            return this.context.Files.FirstOrDefault(n => n.Id == id);
        }

        public IEnumerable<ComicBookFile> FindByName(string search)
        {
            return this.context.Files
                .Where(n => n.Name.ToUpper().Contains(search.ToUpper()));
        }
    }
}