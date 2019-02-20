using ComicViewer.Core;
using ComicViewer.Core.Interigators;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;

namespace ComicViewer.Tests
{
    public class OneThousandFileIntegationTest
    {
        public static class SampleNameFile
        {
            public static IEnumerable<ComicBookFile> Load(IEnumerable<IComicInterigator> interigators)
            {
                return File.ReadAllLines("ExamplePaths.txt")
                    .Select(line => new FileInfo(line))
                    .Select(file => interigators.Aggregate(new ComicBookFile(), (comic, interigator) => {
                        interigator.Apply(comic, file, null, null);
                        return comic;
                    }));
            }            
        }        
        
        [Fact]
        public void OneThousandFileIntegationTest_Publisher()
        {
            var comics = SampleNameFile.Load(new IComicInterigator[] {
                new IdInterigator(),
                new PublisherInterigator()                
            });

            Assert.DoesNotContain(comics, n => string.IsNullOrEmpty(n.Publisher));            
        }

        [Fact]
        public void OneThousandFileIntegationTest_Volume()
        {
            var comics = SampleNameFile.Load(new IComicInterigator[] {
                new IdInterigator(),                
                new VolumeInterigator()                
            });
            var list = comics.Where(n => string.IsNullOrEmpty(n.Volume)).Select(n => n.Path);
            Assert.DoesNotContain(comics, n => string.IsNullOrEmpty(n.Volume));
            
        }
    }
}
