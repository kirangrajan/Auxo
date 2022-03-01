using System.IO;
using System.Threading.Tasks;

using Auxo.Prototype.DomainModel;

using ImageMagick;

namespace Auxo.Prototype.Services
{
    /// <summary>
    /// Image processor using MagicNet
    /// </summary>
    public class MagicNetImageProcessor : IMagicNetImageProcessor
    {
        /// <summary>
        /// Re size an image
        /// </summary>
        /// <param name="request">Process image request</param>
        /// <param name="imageStream">Image stream</param>
        /// <returns>Image Process response</returns>
        public async Task<ImageProcessResponse> ResizeImage(ImageProcessRequest request, Stream imageStream)
        {
            imageStream.Seek(0, SeekOrigin.Begin);
            var stream = new MemoryStream();
            using (var image = new MagickImage(imageStream))
            {
                var size = new MagickGeometry(request.MaximumWidth.ToInt(), request.MinimumHeight.ToInt())
                               {
                                   IgnoreAspectRatio = false
                               };

                image.Resize(size);

                // Save the result
                image.Write(stream);
            }
            
            stream.Seek(0, SeekOrigin.Begin);
            using (var image = new MagickImage(stream))
            {
                var optimizer = new ImageOptimizer();
                optimizer.LosslessCompress(stream);

                // Save the result
                image.Write(request.FileName);
            }

            return new ImageProcessResponse
                       {
                           Status = "Success",
                           FileLocation = Path.GetDirectoryName(request.FileName),
                           Filename = Path.GetFileName(request.FileName),
                           FileHeight = request.MinimumWidth.ToInt(),
                           FileWidth = request.MinimumHeight.ToInt()
                       };
        }
    }
}