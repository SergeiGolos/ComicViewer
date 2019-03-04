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

        public int Length { get; set; }

        #endregion        

        #region Parsed Metadata

        public string Publisher { get; set; }

        public string Name { get; set; }

        public string Year { get; set; }

        public string Month { get; set; }

        public string Volume { get; set; }

        public string Issue { get; set; }

        public string Title { get; internal set; }        

        #endregion 
    }
}