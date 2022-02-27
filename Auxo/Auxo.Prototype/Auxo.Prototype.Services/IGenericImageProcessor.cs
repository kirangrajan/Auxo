using System.IO;
using System.Threading.Tasks;

using Auxo.Prototype.DomainModel;

namespace Auxo.Prototype.Services
{
    /// <summary>
    /// Contract for Core processor for Images using C# libraries
    /// </summary>
    public interface IGenericImageProcessor
    {

        /// <summary>
        /// Re size an image
        /// </summary>
        /// <param name="request">Process image request</param>
        /// <param name="imageStream">Image stream</param>
        /// <returns>Image Process response</returns>
        Task<ImageProcessResponse> ResizeImageAsJpeg(ImageProcessRequest request, Stream imageStream);

        ///// <summary>
        ///// Adjust scale factor of the image
        ///// </summary>
        ///// <param name="request">Process image request</param>
        ///// <param name="imageHeight">Height of the image</param>
        ///// <param name="imageWidth">Width of the image</param>
        ///// <returns>Updated Image height and width</returns>
        //Task AdjustScaleFactor(ImageProcessRequest request, int imageHeight, int imageWidth);
    }
}