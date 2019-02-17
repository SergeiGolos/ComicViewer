namespace ComicViewer.Core.Configuration
{
	public class ComicViewerConfiguration
	{
        public string ComicRepositoryPath { get; set; }
        public string DatabasePath { get; set; }

        public int? ThumbnailHeight { get; set; }
		public int? ThumbnailWidth { get; set; }		
	}
}