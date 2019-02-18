namespace ComicViewer.Core.Indexers
{
    using ComicViewer.Core.Configuration;
    using System;

    public class StoreIndexer : BaseComicIndexer, IComicIndexer
    {
        private readonly ComicViewerConfiguration config;
        private readonly ComicBookContext context;

        public StoreIndexer(ComicViewerConfiguration config, IComicBookFactory factory, ComicBookContext context) : base(config, factory)
        {
            this.config = config;
            this.context = context;
        }

        public override void Store(ComicBookFile file)
        {
            // TODO validation if this should be saved or updated.
            var reletivePath = file.Path.Replace(config.ComicRepositoryPath, string.Empty);
            Console.WriteLine(file.Id+ ": " + reletivePath);
            context.Files.Add(file);
            context.SaveChanges();
        }
    }
}
