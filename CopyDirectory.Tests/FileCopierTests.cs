using CopyDirectory.ConsoleApp.Extension;
using CopyDirectory.Services;
using CopyDirectory.Services.Wrappers;
using CopyDirectory.Shared.Config;
using Moq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using Xunit;

namespace FileCopy.Tests
{
    public class FileCopierTests
    {

        [Fact]
        public void When_SourceEqualsDestination_ValidationError()
        {
            var cliOptionsMock = new CLIOptions
            {
                Source = new Uri("C:/"),
                Destination = new Uri("C:/")
            };
            ICollection<ValidationResult> validationResults;
            cliOptionsMock.ValidateObject(out validationResults);

            //we should actually the messages here..
            Assert.Equal(1, validationResults.Count);
        }

        [Fact]
        public void When_SourceDoesNotExits_ValidationError()
        {
            var cliOptionsMock = new CLIOptions
            {
                Source = new Uri("C:/sdfsdfgdfgfg"),
                Destination = new Uri("C:/")
            };
            ICollection<ValidationResult> validationResults;
            cliOptionsMock.ValidateObject(out validationResults);

            //we should actually the messages here..
            Assert.Equal(1, validationResults.Count);
        }

        [Fact]
        public void When_DestinationDirectyNotExist_Create()
        {
            var FileDirectoryMock = new Mock<IDirectoryWrapper>();
            var PathWrapperMock = new Mock<IPathWrapper>();
            var ConsoleMessageHandler = new Mock<IMessageHandler>();
            FileDirectoryMock.Setup(x => x.Exists(It.IsAny<string>())).Returns(false);
            FileDirectoryMock.Setup(x => x.CreateDirectory(It.IsAny<string>())).Returns(new DirectoryInfo("C:/sometestPath"));
            FileDirectoryMock.Setup(x => x.GetAllDirectories(It.IsAny<string>(), null)).Returns(Array.Empty<string>());
            FileDirectoryMock.Setup(x => x.GetFiles(It.IsAny<string>(), It.IsAny<IEnumerable<string>>(), null)).Returns(Array.Empty<string>());

            FileCopier copier = new FileCopier(FileDirectoryMock.Object, PathWrapperMock.Object, ConsoleMessageHandler.Object);

            copier.Execute(new CLIOptions
            {
                Source = new Uri("C:/somepath"),
                Destination = new Uri("C:/someotherpath")
            });

            FileDirectoryMock.Verify(x => x.CreateDirectory(It.IsAny<string>()), Times.Once);
        }
    }
}
