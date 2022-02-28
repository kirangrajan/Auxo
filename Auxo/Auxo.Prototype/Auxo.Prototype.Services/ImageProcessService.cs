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

        private readonly IGenericImageProcessor _genericImageProcessor;

        public ImageProcessService(IGenericImageProcessor genericImageProcessor)
        {
            this._genericImageProcessor = genericImageProcessor;
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

            var response = await this._genericImageProcessor.ResizeImageAsJpeg(request, stream);

            return response;
        }

        private static ImageCodecInfo GetEncoderInfo(string mimeType)
        {
            ImageCodecInfo[] encoders;
            encoders = ImageCodecInfo.GetImageEncoders();
            foreach (ImageCodecInfo ici in encoders)
            {
                if (ici.MimeType == mimeType)
                {
                    return ici;
                }
            }

            return null;
        }
    }
}
