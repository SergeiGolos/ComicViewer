using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using SharpCompress.Archives;

namespace ComicViewer.Core.Interigators
{
    public abstract class RegExInterigator : IComicInterigator
    {
        public class RegExInterigatorContext
        {
            public ComicBookFile comic { get; set; }
            public FileInfo file { get; set; }
            public IArchive archive { get; set; }
            public IEnumerable<IArchiveEntry> pages { get; set; }
        }

        private readonly Dictionary<string, Func<RegExInterigatorContext, string>> resolvers;
        private readonly Dictionary<string, Func<ComicBookFile, MatchCollection, bool>> binders;

        public RegExInterigator()
        {
            this.resolvers= new Dictionary<string, Func<RegExInterigatorContext, string>>();
            this.binders = new Dictionary<string, Func<ComicBookFile, MatchCollection, bool>>();
        }

        protected void Test(string regEx, Func<RegExInterigatorContext, string> resolver, Func<ComicBookFile, MatchCollection, bool> binder)
        {
            this.resolvers.Add(regEx, resolver);
            this.binders.Add(regEx, binder);
        }

        public virtual void Apply(ComicBookFile comic, FileInfo file, IArchive archive, IEnumerable<IArchiveEntry> pages)
        {
            var context = new RegExInterigatorContext()
            {
                comic = comic,
                file = file,
                archive = archive,
                pages = pages
            };            
            foreach (var expression in resolvers)
            {
                var test = new Regex(expression.Key);
                var testValue = expression.Value(context);
                var matches = test.Matches(testValue);

                if (!Apply(comic, expression.Key,  matches))
                {
                    break;
                }
            }            
        }

        public virtual bool Apply(ComicBookFile comic, string test, MatchCollection matches)
        {
            if (matches.Count > 0)
            {
                return this.binders[test](comic, matches);
            }
            return true;
        }
    }
}
