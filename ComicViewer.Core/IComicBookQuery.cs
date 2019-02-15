using System;
using System.Collections.Generic;
using System.Text;

namespace ComicViewer.Core
{
	public interface IComicBookQuery
	{
		IEnumerable<ComicBookFileInfo> Search(string term);
	}
}