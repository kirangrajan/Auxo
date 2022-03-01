using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;

using Auxo.Prototype.DomainModel;

namespace Auxo.Prototype.Services
{
    /// <summary>
    /// Image Process Service
    /// </summary>
    public class ImageProcessService : IImageProcessService
    {
        private const int OptimumWidth = 800;

        private const int OptimumHeight = 800;

        private const string DefaultSaveLocation = @"c:\temp\";

        private readonly IBaseImageProcessor _imageProcessor;

        private readonly IBaseImageProcessor _magicNetImageProcessor;

        public ImageProcessService(IGenericImageProcessor imageProcessor, IMagicNetImageProcessor magicNetImageProcessor)
        {
            this._imageProcessor = imageProcessor;
            this._magicNetImageProcessor = magicNetImageProcessor;
        }

        /// <summary>
        /// Process a given image
        /// </summary>
        /// <param name="request">Image process request</param>
        /// <param name="stream">Image - memory stream</param>
        /// <returns>Task details</returns>
        public async Task<ImageProcessResponse> ProcessImage(ImageProcessRequest request, Stream stream)
        {
            if (request.MinimumHeight < OptimumHeight)
            {
                request.MinimumHeight = OptimumHeight;
            }

            if (request.MinimumWidth < OptimumWidth)
            {
                request.MinimumWidth = OptimumWidth;
            }

            request.FileName = $"{DefaultSaveLocation}{request.FileName}_Resized{request.FileExtension}";

            var response = await this._imageProcessor.ResizeImage(request, stream);

           // var response = await this._magicNetImageProcessor.ResizeImage(request, stream);

            return response;
        }
    }
}
