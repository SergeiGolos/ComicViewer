using System;
using System.Collections.Generic;
using System.IO;

namespace ComicViewer.Core
{

    public interface IComicBookIndexResolver
    {
        IEnumerable<ComicBookFileInfo> FindByName(string search);
        ComicBookFileInfo FindById(string id);

        IComicBookIndexResolver Index();
    }
}