using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using SharpCompress.Archives;

namespace ComicViewer.Core.Interigators
{
    public class IssueInterigator : RegExInterigator, IComicInterigator
    {
        public IssueInterigator()
        {
            Test("\b[0-9]{2,3}\b", c => c.comic.OriginalName, ApplyIssue);
        }        
        
        public bool ApplyIssue(ComicBookFile comic, MatchCollection matches)
        {
            comic.Volume = matches[0].Value;
            return true;
        }
    }
}
