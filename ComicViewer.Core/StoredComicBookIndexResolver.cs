using ComicViewer.Core.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ComicViewer.Core
{
	public class StoredComicBookIndexResolver : IComicBookIndexResolver
	{
		private readonly IComicBookFactory factory;
		private readonly IImageProcessor processor;
		private readonly IComicBookQuery query;
		private readonly IComicBookCommand command;
		private readonly ComicViewerConfiguration config;

		public StoredComicBookIndexResolver(IComicBookFactory factory, IImageProcessor processor, IComicBookQuery query, IComicBookCommand command, ComicViewerConfiguration config)
		{
			this.factory = factory;
			this.processor = processor;
			this.query = query;
			this.command = command;
			this.config = config;
		}

		public ComicBookFileInfo FindById(string id)
		{
			var foundBook = this.query.Search(id);

			return foundBook != null
			   ? foundBook.First()
			   : null;
		}

		public IEnumerable<ComicBookFileInfo> FindByName(string search)
		{
			var foundBooks = this.query.Search(search);
			return foundBooks != null
				? foundBooks
				: null;

			//   .Where(file => file.Value.Name.IndexOf(search, StringComparison.CurrentCultureIgnoreCase) >= 0)
			// .Select(file => new ComicBookFileInfo(file.Key, file.Value));
		}

		public IComicBookIndexResolver Index()
		{
			var rootPath = new DirectoryInfo(config.ComicRepositoryPath);
			var files = rootPath.GetFiles("*.cbr", SearchOption.AllDirectories);
			foreach (var file in files)
			{
				var key = file.GetHashCode().ToString("X");
				var book = this.factory.LoadFile(file);
				var thumbnail = GenerateThumbnail(book);
				book.Thumbnail = thumbnail;
				this.command.SaveBooksToDatabaseAsync(new ComicBookFileInfo(key, book)).Start();
			}

			//return this;
			throw new NotImplementedException();
		}

		private byte[] GenerateThumbnail(ComicBookFile info)
		{
			var image = factory.LoadPage(info.GetFileInfo(), 0);
			if (image == null) { return new byte[0]; }
			if (config.ThumbnailHeight.HasValue && config.ThumbnailWidth.HasValue)
			{
				image = this.processor.Resize(image, config.ThumbnailHeight.Value, config.ThumbnailWidth.Value);
			}
			var bytes = new MemoryStream();
			image.CopyToAsync(bytes);
			return bytes.ToArray();
		}
	}
}