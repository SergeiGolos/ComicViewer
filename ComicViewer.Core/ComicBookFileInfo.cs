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
        }

        public string Id { get; set; }


    }
}