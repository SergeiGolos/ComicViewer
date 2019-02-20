using ComicViewer.Core;
using ComicViewer.Core.Interigators;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ComicViewer.Tests
{
    public class VolumeInterigationTests
    {
        [Fact]
        public void VolumeIsNotDefined()
        {
            var comicbook = new ComicBookFile()
            {
                OriginalName = "File with No Volume"
            };
            new VolumeInterigator().Apply(comicbook, null, null, null);

            Assert.True(string.IsNullOrEmpty(comicbook.Volume));
        }

        [Theory]
        [InlineData("File with Volume v2", "v2")]
        public void VolumeIsNotDefinedByDefault(string fileName, string volume)
        {
            var comicbook = new ComicBookFile()
            {
                OriginalName = fileName
            };
            new VolumeInterigator().Apply(comicbook, null, null, null);

            Assert.Equal(volume, comicbook.Volume);
        }
    }
}
