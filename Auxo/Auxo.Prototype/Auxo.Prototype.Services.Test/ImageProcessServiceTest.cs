using System.IO;
using System.Threading.Tasks;

using Auxo.Prototype.DomainModel;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace Auxo.Prototype.Services.Test
{
    /// <summary>
    /// Tests for Image Process Service
    /// </summary>
    [TestClass]
    public class ImageProcessServiceTest
    {
        private Mock<IGenericImageProcessor> _genericImageProcessor;

        [TestMethod]
        public async Task Should_Rename_File_Before_Saving()
        {
            // Arrange
            this._genericImageProcessor = new Mock<IGenericImageProcessor>();
            var request = new ImageProcessRequest
                              {
                                  MinimumHeight = 10,
                                  MinimumWidth = 10,
                                  FileExtension = ".jpg",
                                  FileName = "c:\\Test.jpg"
                              };
            this._genericImageProcessor
                .Setup(p => p.ResizeImageAsJpeg(It.IsAny<ImageProcessRequest>(), It.IsAny<Stream>())).ReturnsAsync(
                    new ImageProcessResponse
                        {
                            FileHeight = 1000, FileWidth = 1000, Filename = "c:\\Test_Revised.jpg"
                        });

            var imageService = new ImageProcessService(this._genericImageProcessor.Object);

            // Act
            var response = await imageService.ProcessImage(request, new MemoryStream());

            // Assert
            Assert.AreNotEqual(response.Filename, request.FileName);
        }

        [TestMethod]
        public async Task Should_Set_File_Width()
        {
            // Arrange
            this._genericImageProcessor = new Mock<IGenericImageProcessor>();
            var request = new ImageProcessRequest
                              {
                                  MinimumHeight = 10,
                                  MinimumWidth = 10,
                                  FileExtension = ".jpg",
                                  FileName = "c:\\Test.jpg"
                              };
            this._genericImageProcessor.Setup(p => p.ResizeImageAsJpeg(It.IsAny<ImageProcessRequest>(), It.IsAny<Stream>())).ReturnsAsync(
                new ImageProcessResponse
                    {
                        FileHeight = 1000,
                        FileWidth = 1000
                    });

            var imageService = new ImageProcessService(this._genericImageProcessor.Object);

            // Act
            var response = await imageService.ProcessImage(request, new MemoryStream());

            // Assert
            Assert.AreNotEqual(response.FileHeight, request.MinimumHeight);
            Assert.AreNotEqual(response.FileWidth, request.MinimumWidth);
        }
    }
}