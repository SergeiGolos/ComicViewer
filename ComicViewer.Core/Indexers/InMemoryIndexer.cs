namespace ComicViewer.Core.Indexers
{
    using System;
    using System.Collections.Generic;
    using ComicViewer.Core.Configuration;


    public class InMemoryIndexer : BaseComicIndexer, IComicIndexer
    {
        private readonly ComicViewerConfiguration config;

        public InMemoryIndexer(ComicViewerConfiguration config, IComicBookFactory factory) : base(config, factory)
        {
            this.Files = new Dictionary<string, ComicBookFile>();
        }

        public Dictionary<string, ComicBookFile> Files { get; set; }

        public override void Store(ComicBookFile file)
        {
            this.Files.Add(file.Id, file);
        }
    }    
}
