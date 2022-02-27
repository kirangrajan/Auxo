using System.IO;
using System.Threading.Tasks;

using Auxo.Prototype.DomainModel;

namespace Auxo.Prototype.Services
{
    /// <summary>
    /// Image Processing Service
    /// </summary>
    public interface IImageProcessService
    {
        /// <summary>
        /// Process a given image
        /// </summary>
        /// <param name="request">Image process request</param>
        /// <param name="stream">Image - Memory stream</param>
        /// <returns>Task details</returns>
        Task<ImageProcessResponse> ProcessImage(ImageProcessRequest request, Stream stream);
    }
}