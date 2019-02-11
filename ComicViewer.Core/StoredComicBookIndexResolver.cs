using System;
using System.Collections.Generic;

namespace ComicViewer.Core
{
    public class StoredComicBookIndexResolver : IComicBookIndexResolver
    {
        private readonly IComicBookFactory factory;
        private readonly IImageProcessor processor;

        public StoredComicBookIndexResolver(IComicBookFactory factory, IImageProcessor processor)
        {
            this.factory = factory;
            this.processor = processor;
        }

        public ComicBookFileInfo FindById(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ComicBookFileInfo> FindByName(string search)
        {
            throw new NotImplementedException();
        }

        public IComicBookIndexResolver Index()
        {
            throw new NotImplementedException();
        }
    }
}