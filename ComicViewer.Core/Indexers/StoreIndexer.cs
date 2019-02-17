namespace ComicViewer.Core.Indexers
{
    using ComicViewer.Core.Configuration;

    public class StoreIndexer : BaseComicIndexer, IComicIndexer
    {
        private readonly ComicBookContext context;

        public StoreIndexer(ComicViewerConfiguration config, IComicBookFactory factory, ComicBookContext context) : base(config, factory)
        {
            this.context = context;
        }

        public override void Store(ComicBookFile file)
        {
            // TODO validation if this should be saved or updated.
            context.Files.Add(file);
            context.SaveChanges();
        }
    }
}
