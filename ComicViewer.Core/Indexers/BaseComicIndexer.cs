namespace ComicViewer.Core.Indexers
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
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

        public virtual bool AlreadyDefined(FileInfo file)
        {
            return false;
        }

        public virtual IComicIndexer Run()
        {
            var comics = this.extensions
                .SelectMany(ext => path.GetFiles(ext, SearchOption.AllDirectories))
                // .AsParallel()
                .Where(file => !AlreadyDefined(file))
                .Select(file => this.factory.LoadFile(file));                

            foreach (var comic in comics) { 
                if (comic == null) { continue; }

                this.Store(comic);                
            }
            
            return this;
        }
    }
}
