namespace ComicViewer.Core
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using Microsoft.Extensions.Caching.Memory;
    using SharpCompress.Archives;

    public class CacheComcicBookFactory : ComicBookFactory
    {
        private IMemoryCache cache;

        public CacheComcicBookFactory(IMemoryCache cache, IImageProcessor processor, IEnumerable<IComicInterigator> interigators) : base(processor, interigators)
        {
            this.cache = cache;
        }

        public override T InArchive<T>(FileInfo file, Func<IArchive, IEnumerable<IArchiveEntry>, T> loaderFn)
        {
            try
            {
                var archive = this.cache.GetOrCreate(file.FullName, entry => {
                    entry.SlidingExpiration = TimeSpan.FromSeconds(60);
                    entry.PostEvictionCallbacks.Add(new PostEvictionCallbackRegistration()
                    {
                        EvictionCallback = (key, value, reason, state) => {
                            var item = value as Tuple<IArchive, IEnumerable<IArchiveEntry>>;
                            item.Item1.Dispose();
                        }
                    });

                    var arch = ArchiveFactory.Open(file.FullName);
                    var pages = LoadPages(arch);
                    return Tuple.Create(arch, pages);
                }); 
                               
                return loaderFn(archive.Item1, archive.Item2);                
            }
            catch (Exception ex)
            {
                return default(T);
            }
        }

        //public override MemoryStream LoadPage(FileInfo file, int pageIndex)
        //{
        //    return cache.GetOrCreate(string.Format("{0}:{1}", file.FullName, pageIndex), entry =>
        //    {
        //        entry.SlidingExpiration = TimeSpan.FromSeconds(15);
        //        entry.PostEvictionCallbacks.Add(new PostEvictionCallbackRegistration()
        //        {
        //            EvictionCallback = (key, value, reason, state) =>
        //            {                         
        //                (value as MemoryStream).Dispose();
        //            }
        //        });

        //        return base.LoadPage(file, pageIndex);
        //    });
        //}
    }
}
