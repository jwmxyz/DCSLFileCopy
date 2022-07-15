using FileCopy.Wrappers;
using System;
using Xunit;

namespace FileCopy.Tests
{
    
    public class PathWrapperTests
    {
        private readonly Uri SOURCE_URI = new("C:/some/test/folder");
        private readonly Uri SOURCE_URI_WITH_TRAILING_SLASH = new("C:/some/test/folder/");
        private readonly PathWrapper _pathWrapper = new();

        [Theory]
        [InlineData("C:\\", "C:\\some\\test\\folder\\Dev\\folder", "C:\\Dev\\folder")]
        [InlineData("C:\\test", "C:\\some\\test\\folder\\desktop.ini", "C:\\test\\desktop.ini")]
        [InlineData("C:\\test\\", "C:\\some\\test\\folder\\desktop.ini", "C:\\test\\desktop.ini")]
        public void PathsCorrectlyCombine(string path1, string path2, string expected)
        {
            Assert.Equal(expected, _pathWrapper.Combine(path1, path2, SOURCE_URI.LocalPath));
        }

        [Theory]
        [InlineData("C:\\", "C:\\some\\test\\folder\\Dev\\folder", "C:\\Dev\\folder")]
        [InlineData("C:\\test", "C:\\some\\test\\folder\\desktop.ini", "C:\\test\\desktop.ini")]
        [InlineData("C:\\test\\", "C:\\some\\test\\folder\\desktop.ini", "C:\\test\\desktop.ini")]
        public void PathsCorrectlyCombineWhenSourceHasTrailingSlash(string path1, string path2, string expected)
        {
            Assert.Equal(expected, _pathWrapper.Combine(path1, path2, SOURCE_URI_WITH_TRAILING_SLASH.LocalPath));
        }
    }
}
