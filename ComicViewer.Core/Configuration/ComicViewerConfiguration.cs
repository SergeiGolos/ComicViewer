using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComicViewer.Core.Configuration
{
	public class ComicViewerConfiguration
	{
		public string ComicRepositoryPath { get; set; }
		public int? ThumbnailHeight { get; set; }
		public int? ThumbnailWidth { get; set; }
		public string DatabasePath { get; set; }
	}
}