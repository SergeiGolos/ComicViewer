using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using SharpCompress.Archives;

namespace ComicViewer.Core.Interigators
{
    public class DateInterigator : RegExInterigator, IComicInterigator
    {
        public DateInterigator()
        {
            Test("\b[0-9]{6}\b", c => c.comic.OriginalName, ApplyYearAndMonth);
            Test("\b[0-9]{4}\b", c => c.comic.OriginalName, ApplyYear);
        }
        public bool ApplyYearAndMonth(ComicBookFile comic, MatchCollection matches)
        {
            var result = matches[0].Value;

            comic.Year = result.Substring(0, 4);
            comic.Month = result.Substring(5);
            return false;
        }

        public bool ApplyYear(ComicBookFile comic, MatchCollection matches)
        {            
            comic.Year = matches[0].Value;            
            return true;
        }    
    }
}
