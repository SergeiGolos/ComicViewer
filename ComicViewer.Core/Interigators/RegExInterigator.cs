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
        private readonly List<Action<RegExInterigatorContext>> setup;
        private readonly List<Action<RegExInterigatorContext>> cleanup;

        public RegExInterigator()
        {
            this.resolvers= new Dictionary<string, Func<RegExInterigatorContext, string>>();
            this.binders = new Dictionary<string, Func<ComicBookFile, MatchCollection, bool>>();
            this.setup = new List< Action<RegExInterigatorContext>>();
            this.cleanup = new List<Action<RegExInterigatorContext>>();
        }

        protected void SetUp(Action<RegExInterigatorContext> binder)
        {
            this.setup.Add(binder);
        }

        protected void Cleanup(Action<RegExInterigatorContext> binder)
        {
            this.cleanup.Add(binder);
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

            foreach(var binder in setup)
            {
                binder(context);
            }

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

            foreach (var binder in cleanup)
            {
                binder(context);
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
