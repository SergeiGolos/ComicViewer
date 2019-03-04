namespace ComicViewer.Core.Indexers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Timers;
    using ComicViewer.Core.Configuration;

    public abstract class BaseComicIndexer : IComicIndexer
    {
        private readonly ComicViewerConfiguration config;
        private readonly IComicBookFactory factory;
        private readonly DirectoryInfo path;
        private readonly IEnumerable<string> extensions;
        protected int TotalFiles;
        protected int CurrentFile;

        public BaseComicIndexer(ComicViewerConfiguration config, IComicBookFactory factory)
        {
            this.extensions = new[] { "*.cbr", "*.cbz", "*.rar", "*.zip" };

            this.path = new DirectoryInfo(config.ComicRepositoryPath);
            this.config = config;
            this.factory = factory;            
        }
        protected string PrecentDone()
        {
            return (((double)this.CurrentFile / (double)this.TotalFiles) * 100).ToString("000.00") + " % ";
        }

        public abstract void Store(ComicBookFile file);

        public virtual bool IsReadyForProcessing(FileInfo file)
        {
            return true;
        }

        public virtual IComicIndexer Run()
        {
            var comicsFiles = this.extensions
                .SelectMany(ext => path.GetFiles(ext, SearchOption.AllDirectories))
                .OrderBy(file => file.FullName);
            this.TotalFiles = comicsFiles.Count();
            this.CurrentFile = 0;

            var files = comicsFiles
                .Do(file => this.CurrentFile++)
                .Where(file => IsReadyForProcessing(file));

            foreach (var file in files)
            {
                Timed(file.FullName, () =>
                {
                    var comic = this.factory.LoadFile(file);
                    if (comic == null) { return; }

                    this.Store(comic);
                });
            }
            
            return this;
        }

        protected void Timed(string name, Action runner)
        {
            var timer = new Timer();

            Console.WriteLine("Starting: {0}", name);
            timer.Start();

            runner();

            timer.Stop();
            Console.WriteLine("Completed in {0}", timer.Interval.ToString());
        }

        protected void WriteLog(ComicBookFile file, string action)
        {
            var reletivePath = file.Path.Replace(config.ComicRepositoryPath, string.Empty);

            action = string.IsNullOrEmpty(action) ? "" : $" - ({action})";
            Console.WriteLine($"({PrecentDone()}) {file.Id} : {reletivePath}{action}");
        }
    }

    public static class DoHelper
    {
        public static IEnumerable<T> Do<T>(this IEnumerable<T> list, Action<T> fn)
        {
            foreach (var item in list)
            {
                fn(item);
                yield return item;
            }
        }
    }
}
