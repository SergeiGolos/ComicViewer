using System;
using System.Collections.Generic;
using System.Text;

namespace ComicViewer.Core
{
	public class ComicBookQuery : IComicBookQuery
	{
		public IEnumerable<ComicBookFileInfo> Search(string term)
		{
			return null;
		}
	}
}