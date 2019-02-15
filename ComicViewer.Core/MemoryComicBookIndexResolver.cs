using ComicViewer.Core.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace ComicViewer.Core
{
    public class MemoryComicBookIndexResolver : IComicBookIndexResolver
    {
        private readonly IComicBookFactory factory;
        private readonly ComicViewerConfiguration config;

        public MemoryComicBookIndexResolver(IComicBookFactory factory, ComicViewerConfiguration config)
        {
            this.Files = new Dictionary<string, ComicBookFile>();
            this.factory = factory;
            this.config = config;
        }

        public Dictionary<string, ComicBookFile> Files { get; set; }

        public ComicBookFileInfo FindById(string id)
        {
            return this.Files.ContainsKey(id)
                ? new ComicBookFileInfo(id, this.Files[id])
                : null;
        }

        public IEnumerable<ComicBookFileInfo> FindByName(string search)
        {
            return this.Files
                .Where(file => file.Value.Name.IndexOf(search, StringComparison.CurrentCultureIgnoreCase) >= 0)
                .Select(file => new ComicBookFileInfo(file.Key, file.Value));
        }

        public IComicBookIndexResolver Index()
        {
            var extentions = new[] { "*.cbr", "*.cbz", "*.rar", "*.zip" };
            var rootPath = new DirectoryInfo(config.ComicRepositoryPath);
            var files = extentions.SelectMany(extension => rootPath.GetFiles(extension, SearchOption.AllDirectories));
            foreach (var file in files)
            {
                var key = file.GetHashCode().ToString("X");
                this.Files.Add(key, this.factory.LoadFile(file));
            }

            return this;
        }
    }
}