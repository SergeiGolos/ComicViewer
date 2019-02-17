namespace ComicViewer.Core
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class ComicBookFile
	{
        [Key]
        public string Id { get; set; }

        public DateTime Created { get; set; }

        public string Path { get; set; }

        public string Name { get; set; }

        public string Extension { get; set; }

        public bool IsSolid { get; set; }

        public int Length { get; set; }

        public int Height { get; set; }

        public int Width { get; set; }

        public byte[] Thumbnail { get; set; }        
	}
}