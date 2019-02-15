namespace ComicViewer.Core
{
	public class ComicBookFileInfo : ComicBookFile
	{
		private string key;
		private ComicBookFile value;

		public ComicBookFileInfo(string key, ComicBookFile value)
		{
			this.Id = key;
			this.Name = value.Name;
			this.NumberOfPages = value.NumberOfPages;
			this.Path = value.Path;
			this.Thumbnail = value.Thumbnail;
		}

		public string Id { get; set; }

		public ComicBookListing GetListing()
		{
			return new ComicBookListing()
			{
				Id = this.Id,
				Name = this.Name,
				Path = this.Path,
				NumberOfPages = this.NumberOfPages,
				Height = this.Height,
				Width = this.Width,
				Thumbnail = this.Thumbnail
			};
		}
	}
}