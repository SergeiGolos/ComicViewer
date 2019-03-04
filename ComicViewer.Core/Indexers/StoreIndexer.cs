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

        public override bool IsReadyForProcessing(FileInfo file)
        {
            if (file.FullName.Contains("fc only")) { return false; }

            var entry = resolver.FindByPath(file.FullName);
            if (entry != null)
            {
                WriteLog(entry, "Skip");
                return false;
            }
            return true;
        }

        public override void Store(ComicBookFile file)
        {                        
            this.resolver.Store(file);            
        }
    }
}
