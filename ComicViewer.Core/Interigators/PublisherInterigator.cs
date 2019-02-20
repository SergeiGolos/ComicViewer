using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using SharpCompress.Archives;

namespace ComicViewer.Core.Interigators
{
    public class PublisherInterigator : IComicInterigator
    {
        protected Dictionary<string, string> publisherMap = new Dictionary<string, string>()
        {
            { "Archie Comics (1939-2016)", "Archie Comics" },
            { "asterix&obelix", "Asterix & Obelix" },
            { "Calvin and Hobbes", "Calvin and Hobbes" },
            {  "Dark Horse Comics (2010-2015)", "Dark Horse Comics" },
            { "DC Chronology", "DC Chronology" },
            { "Dynamite","Dynamite" },
            { "Futurama","Futurama" },
            { "Ghost in the Shell","Ghost in the Shell" },
            { "IDW Comics", "IDW Comics" },
            { "Image Comics 2010-2015", "Image Comics" },
            { "Marvel Chronology", "Marvel Chronology" },
            { "Top Cow (1992-2016)", "Top Cow" },
            { "Valiant Comics (1992-2017)", "Valiant Comics" },
            { "Vertigo", "Vertigo" },
            { "Zenescope" ,"Zenescope" },

        };

        public void Apply(ComicBookFile comic, FileInfo file, IArchive archive, IEnumerable<IArchiveEntry> pages)
        {
            comic.Publisher = publisherMap.Where(n => comic.Path.Contains(n.Key))
                .Select(n => n.Value)
                .FirstOrDefault() ?? string.Empty;
            }
    }
}
