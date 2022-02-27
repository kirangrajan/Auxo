using System.IO;
using System.Threading.Tasks;

using Auxo.Prototype.DomainModel;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Auxo.Prototype.Services.Test
{
    /// <summary>
    /// Tests for GenericImageProcessor
    /// </summary>
    [TestClass]
    public class GenericImageProcessorTest
    {
        [TestMethod]
        public async Task Should_Only_Support_Selected_File_Types()
        {
            // Arrange
            var request = new ImageProcessRequest
                              {
                                  MinimumHeight = 1000,
                                  MinimumWidth = 50,
                                  FileExtension = ".bmp",
                                  FileName = @"c:\test.bmp"
                              };
            var processor = new GenericImageProcessor();

            // Act
            var response = await processor.ResizeImageAsJpeg(request, new MemoryStream());

            // Assert
            Assert.AreEqual("Unsupported file type", response.FailureReason);
        }
    }
}
