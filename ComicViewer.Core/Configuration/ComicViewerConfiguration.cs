using System.Collections.Generic;

namespace ComicViewer.Core.Configuration
{
	public class ComicViewerConfiguration
	{
        public string ComicRepositoryPath { get; set; }
        public string DatabasePath { get; set; }

        public Dictionary<string,int> ThumbnailHeight { get; set; }		
	}
}