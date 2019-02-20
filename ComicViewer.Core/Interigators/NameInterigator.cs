using System.Text.RegularExpressions;

namespace ComicViewer.Core.Interigators
{

    public class NameInterigator : RegExInterigator, IComicInterigator
    {
        public NameInterigator()
        {
            SetUp(c => c.comic.Name = c.comic.OriginalName);

            Test(@"\[[\w|\s|\-|\+|\&|\.]*\]", c => c.comic.Name, ApplyRemoveFound);
            Test(@"\([\w|\s|\-|\+|\&|\.]*\)", c => c.comic.Name, ApplyRemoveFound);
            Test(@"\b[0-9]{2,6}\b", c => c.comic.Name, ApplyRemoveFound);
            Test(@"vol\.[\s*][0-9]+", c => c.comic.Name, ApplyRemoveFound);
            Test(@"v[0-9]+", c => c.comic.Name, ApplyRemoveFound);
            Test(@"--+", c => c.comic.Name, ApplyRemoveFound);

            Cleanup(c => c.comic.Name = c.comic.Name.Trim());
        }
        
        public bool ApplyRemoveFound(ComicBookFile comic, MatchCollection matches)
        {
            for(var index = matches.Count-1; index >= 0; index--)
            {
                comic.Name = comic.Name.Remove(matches[index].Index, matches[index].Length);
            }
            return true;
        }
    }
}
