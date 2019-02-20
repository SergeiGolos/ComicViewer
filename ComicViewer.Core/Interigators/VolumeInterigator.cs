using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using SharpCompress.Archives;

namespace ComicViewer.Core.Interigators
{
    public class VolumeInterigator : RegExInterigator, IComicInterigator
    {
        public VolumeInterigator()
        {
            Test("v[0-9]+", c => c.comic.OriginalName, ApplyVolume);
        }

        public bool ApplyVolume(ComicBookFile comic, MatchCollection matches)
        {            
            comic.Volume = matches[0].Value;
            return true;
        }
    }
}
