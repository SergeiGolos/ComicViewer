using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ComicViewer.Core
{
	public interface IComicBookCommand
	{
		Task SaveBooksToDatabaseAsync(ComicBookFileInfo books);
	}
}