namespace ComicViewer.Core.Indexers
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using ComicViewer.Core.Configuration;

    public abstract class BaseComicIndexer : IComicIndexer
    {
        private readonly ComicViewerConfiguration config;
        private readonly IComicBookFactory factory;
        private readonly DirectoryInfo path;
        private readonly IEnumerable<string> extensions;

        public BaseComicIndexer(ComicViewerConfiguration config, IComicBookFactory factory)
        {
            this.extensions = new[] { "*.cbr", "*.cbz", "*.rar", "*.zip" };

            this.path = new DirectoryInfo(config.ComicRepositoryPath);
            this.config = config;
            this.factory = factory;            
        }

        public abstract void Store(ComicBookFile file);

        public virtual IComicIndexer Run()
        {
            var comics = this.extensions
                .SelectMany(ext => path.GetFiles(ext, SearchOption.AllDirectories))
                .Select(file => this.factory.LoadFile(file))
                .Where(file => file != null);

            foreach (var comic in comics)
            {
                this.Store(comic);
            }

            return this;
        }
    }
}
