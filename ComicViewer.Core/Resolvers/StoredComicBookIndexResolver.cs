namespace ComicViewer.Core
{
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
            return this.context.WithFiles(files => files.FirstOrDefault(n => n.Id == id));
        }

        public IEnumerable<ComicBookFile> FindByName(string search)
        {
            return this.context.WithFiles(files => files.Where(n => n.Name.ToUpper().Contains(search.ToUpper())));
        }        

        public ComicBookFile FindByPath(string path)
        {
            return this.context.WithFiles(files => files.Where(n => n.Path == path).FirstOrDefault());
        }

        public void Store(ComicBookFile file)
        {
            this.context.Add(file);
            this.context.SaveChanges();
        }
    }
}