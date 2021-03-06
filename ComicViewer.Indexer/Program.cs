﻿using ComicViewer.Core;
using ComicViewer.Core.Configuration;
using ComicViewer.Core.Indexers;
using ComicViewer.Core.Interigators;
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
                            ThumbnailHeight = new System.Collections.Generic.Dictionary<string, int>
                            {
                                {"small", 80 },
                                { "large", 300 }
                            }  
                       };
                       var context = new ComicBookContext(config, new Microsoft.EntityFrameworkCore.DbContextOptions<ComicBookContext>());
                       var imgProcessor = new ImageSharpProcessor(config);
                       var factory = new ComicBookFactory(imgProcessor, new IComicInterigator[] {
                           new IdInterigator(),
                           new ImageInterigator(config, context, imgProcessor),
                           new VolumeInterigator(),
                           new IssueInterigator(),
                           new DateInterigator(),
                           new PublisherInterigator(),
                           new NameInterigator(),
                           new TitleInterigator()
                        });
                       var resolver = new StoreComicBookResolver(config, context);
                       Console.WriteLine("Starting Index");
                       var indexer = new StoreIndexer(config, factory, resolver).Run();                       
                   })
                   .WithNotParsed((errs) => { Console.WriteLine("Could not parse arguments");  });

#if DEBUG 
            Console.WriteLine("Press any key to close.");
            Console.ReadKey();
#endif
        }
    }
}
