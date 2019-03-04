using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ComicViewer.Core.Model
{
    public class ComicPageFile
    {
        [Key]
        public int Id { get; set; }

        public string ComicId { get; set; }
        public int Page { get; set; }
        
        public string FileNameMask { get; set; }
        public string Extentions { get; set; }

        public int Height { get; set; }
        public int Width { get; set; }
    }
}
