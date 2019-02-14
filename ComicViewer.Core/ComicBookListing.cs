using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ComicViewer.Core
{
	public class ComicBookListing
	{
		[Key]
		public string Id { get; set; }

		public string Name { get; set; }

		public string Path { get; set; }

		public int NumberOfPages { get; set; }

		public int Height { get; set; }

		public int Width { get; set; }

		public byte[] Thumbnail { get; set; }

		public DateTime Created { get; set; }

		public DateTime Updated { get; set; }
	}
}