using FileCopy.Config;
using FileCopy.Wrappers;
using Moq;
using System;
using System.IO;
using Xunit;

namespace FileCopy.Tests
{
    public class FileCopierTests
    {

        [Fact]
        public void When_SourceEqualsDestination_ExceptionThrown()
        {
            var cliOptionsMock = new CLIOptions
            {
                Source = new Uri("C:/test"),
                Destination = new Uri("C:/test")
            };

            Assert.Throws<Exception>(() => Program.RunProgram(cliOptionsMock));
        }

        [Fact]
        public void When_DestinationDirectyNotExist_Create()
        {
            var FileDirectoryMock = new Mock<IDirectoryWrapper>();
            var PathWrapperMock = new Mock<IPathWrapper>();
            FileDirectoryMock.Setup(x => x.Exists(It.IsAny<string>())).Returns(false);
            FileDirectoryMock.Setup(x => x.CreateDirectory(It.IsAny<string>())).Returns(new DirectoryInfo("C:/sometestPath"));
            FileDirectoryMock.Setup(x => x.GetAllDirectories(It.IsAny<string>())).Returns(Array.Empty<string>());
            FileDirectoryMock.Setup(x => x.GetFiles(It.IsAny<string>())).Returns(Array.Empty<string>());

            FileCopier copier = new FileCopier(FileDirectoryMock.Object, PathWrapperMock.Object);

            copier.Execute(new CLIOptions
            {
                Source = new Uri("C:/somepath"),
                Destination = new Uri("C:/someotherpath")
            });

            FileDirectoryMock.Verify(x => x.CreateDirectory(It.IsAny<string>()), Times.Once);
        }
    }
}
