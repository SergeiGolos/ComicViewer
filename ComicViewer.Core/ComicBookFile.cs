﻿using System;
using System.IO;

namespace ComicViewer.Core
{
	public class ComicBookFile
	{
		public string Name { get; set; }

		public string Path { get; set; }

		public int NumberOfPages { get; set; }

		public int Height { get; set; }

		public int Width { get; set; }

		public byte[] Thumbnail { get; set; }

		public FileInfo GetFileInfo()
		{
			return new FileInfo(this.Path);
		}
	}
}