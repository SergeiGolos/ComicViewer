using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComicViewer.Core
{
	public class ComicBookCommand : IComicBookCommand
	{
		private readonly ComicBookContext context;

		//initialize database connection/source
		public ComicBookCommand(ComicBookContext context)
		{
			this.context = context;
		}

		public async Task SaveBooksToDatabaseAsync(ComicBookFileInfo books)
		{
			using (var db = this.context)
			{
				db.Add(books.GetListing());
				await db.SaveChangesAsync();
			}
		}
	}
}