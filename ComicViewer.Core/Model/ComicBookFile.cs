namespace ComicViewer.Core
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class ComicBookFile
	{
        [Key]
        public string Id { get; set; }

        public DateTime Created { get; set; }

        #region Source Data        

        public string OriginalName { get; set; }

        public string Path { get; set; }

        public string Extension { get; set; }

        #endregion

        #region Page Information 

        public byte[] Thumbnail { get; set; }

        public int Length { get; set; }

        public int Height { get; set; }

        public int Width { get; set; }

        #endregion

        #region Parsed Metadata

        public string Publisher { get; set; }

        public string Name { get; set; }

        public string Year { get; set; }

        public string Month { get; set; }

        public string Volume { get; set; }

        public string Issue { get; set; }                

        #endregion
    }
}