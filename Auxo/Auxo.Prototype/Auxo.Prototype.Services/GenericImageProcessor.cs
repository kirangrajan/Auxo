using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;

using Auxo.Prototype.DomainModel;

namespace Auxo.Prototype.Services
{
    /// <summary>
    ///  Core processor for Images using C# libraries
    /// </summary>
    public class GenericImageProcessor : IGenericImageProcessor
    {
        private const long OptimumImageQuality = 90; // Should be between 1-100

        private const string SupportedMimeType = "image/jpeg";

        private Dictionary<string, ImageFormat> supportedImageTypes = new Dictionary<string, ImageFormat>
                                                                          {
                                                                              { ".jpg", ImageFormat.Jpeg },
                                                                              { ".jpeg", ImageFormat.Jpeg },
                                                                              { ".png", ImageFormat.Png },
                                                                          };

        /// <summary>
        /// Re size an image
        /// </summary>
        /// <param name="request">Process image request</param>
        /// <param name="imageStream">Image stream</param>
        /// <returns>Image Process response</returns>
        public async Task<ImageProcessResponse> ResizeImage(ImageProcessRequest request, Stream imageStream)
        {
            if (!await this.IsSupportedFileType(request.FileExtension))
            {
                return new ImageProcessResponse { Status = "Failure", FailureReason = "Unsupported file type" };
            }

            using (var image = Image.FromStream(imageStream))
            {
                var scaleInfo = this.CalculateScaleFactor(request, image.Height, image.Width);
                using (var bitmap = new Bitmap(scaleInfo.CalculatedWidth.ToInt(), scaleInfo.CalculatedHeight.ToInt()))
                {
                    var imageCodecInfo = this.GetEncoderDetails(SupportedMimeType);
                    var encoder = Encoder.Quality;
                    var encoderParameters = new EncoderParameters(1);
                    var encoderParameter = new EncoderParameter(encoder, OptimumImageQuality);
                    encoderParameters.Param[0] = encoderParameter;

                    using (var graphics = Graphics.FromImage(bitmap))
                    {
                        graphics.SmoothingMode =
                            SmoothingMode
                                .HighQuality; // specifies whether lines, curves, and the edges of filled areas use smoothing (also called antialiasing) 
                        graphics.InterpolationMode =
                            InterpolationMode
                                .HighQualityBicubic; // determines how intermediate values between two endpoints are calculated
                        graphics.CompositingMode =
                            CompositingMode
                                .SourceCopy; // determines whether pixels from a source image overwrite or are combined with background pixels
                        graphics.CompositingQuality =
                            CompositingQuality.HighQuality; // determines the rendering quality level of layered images.
                        graphics.PixelOffsetMode =
                            PixelOffsetMode.HighQuality; // affects rendering quality when drawing the new image

                        using (var wrapMode = new ImageAttributes())
                        {
                            wrapMode.SetWrapMode(WrapMode.TileFlipXY); // prevents ghosting around the image borders
                            graphics.DrawImage(
                                image,
                                0,
                                0,
                                request.MinimumWidth.ToInt(),
                                request.MinimumHeight.ToInt());
                        }

                        bitmap.Save(request.FileName, imageCodecInfo, encoderParameters);
                    }
                }
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

        private (double CalculatedWidth, double CalculatedHeight) CalculateScaleFactor(ImageProcessRequest request, double imageWidth, double imageHeight)
        {
            var aspectRatio = imageWidth / imageHeight;
            var boxRatio = request.MaximumWidth / request.MinimumHeight;

            double scaleFactor = 0;

            if (boxRatio > aspectRatio) 
            {
                scaleFactor = request.MinimumHeight / imageHeight;

            }
            else
            {
                scaleFactor = request.MinimumWidth / imageWidth;
            }

            return (imageWidth * scaleFactor, imageHeight * scaleFactor);
        }

        ///// <summary>
        ///// Is the file type supported for processing
        ///// </summary>
        ///// <param name="fileType">File extension</param>
        ///// <returns>Supported or not</returns>
        private Task<bool> IsSupportedFileType(string fileType)
        {
            var isSupported = false;
            this.supportedImageTypes.TryGetValue(fileType, out var imageFormat);
            if (imageFormat != null)
            {
                isSupported = true;
            }

            return Task.FromResult(isSupported);
        }

        private ImageCodecInfo GetEncoderDetails(string supportedMimeType)
        {
            var encoders = ImageCodecInfo.GetImageEncoders();
            foreach (var ici in encoders)
            {
                if (ici.MimeType == supportedMimeType)
                {
                    return ici;
                }
            }

            return null;
        }
    }
}
