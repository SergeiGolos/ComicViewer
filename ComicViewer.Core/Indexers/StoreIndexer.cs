namespace ComicViewer.Core.Indexers
{
    using ComicViewer.Core.Configuration;
    using System;
    using System.IO;
    using System.Linq;

    public class StoreIndexer : BaseComicIndexer, IComicIndexer
    {
        private readonly ComicViewerConfiguration config;
        private readonly IComicBookResolver resolver;

        public StoreIndexer(ComicViewerConfiguration config, IComicBookFactory factory, IComicBookResolver resolver) : base(config, factory)
        {
            this.config = config;            
            this.resolver = resolver;
        }

        public override bool AlreadyDefined(FileInfo file)
        {
            var entry = resolver.FindByPath(file.FullName);
            if (entry == null) return false;

            var reletivePath = entry.Path.Replace(config.ComicRepositoryPath, string.Empty);
            Console.WriteLine("Skip - " + entry.Id + ": " + reletivePath);
            return true;
        }

        public override void Store(ComicBookFile file)
        {
            // TODO validation if this should be saved or updated.
            var reletivePath = file.Path.Replace(config.ComicRepositoryPath, string.Empty);
            Console.WriteLine(file.Id+ ": " + reletivePath);
            this.resolver.Store(file);            
        }
    }
}
