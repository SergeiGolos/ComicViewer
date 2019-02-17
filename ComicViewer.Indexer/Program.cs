using ComicViewer.Core;
using ComicViewer.Core.Configuration;
using ComicViewer.Core.Indexers;
using CommandLine;
using System;

namespace ComicViewer.Indexer
{
    class Program
    {
        static void Main(string[] args)
        {

            Parser.Default.ParseArguments<IndexerArguments>(args)
                   .WithParsed(o =>
                   {
                       var config = new ComicViewerConfiguration()
                       {
                            ComicRepositoryPath = o.IndexPath,
                            DatabasePath = o.Database,
                            ThumbnailHeight = o.Height,
                            ThumbnailWidth = o.Width
                       };
                       var context = new ComicBookContext(config, new Microsoft.EntityFrameworkCore.DbContextOptions<ComicBookContext>());
                       var imgProcessor = new ImageSharpProcessor();
                       var factory = new ComicBookFactory(imgProcessor, new IComicInterigator[] {
                           new IdInterigator(),
                           new ImageInterigator(config, imgProcessor)
                        });

                       var indexer = new StoreIndexer(config, factory, context).Run();                       
                   })
                   .WithNotParsed((errs) => { Console.WriteLine("Could not parse arguments");  });

#if DEBUG 
            Console.WriteLine("Press any key to close.");
            Console.ReadKey();
#endif
        }
    }
}
