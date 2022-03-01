using System.IO;
using System.Threading.Tasks;

using Auxo.Prototype.DomainModel;

namespace Auxo.Prototype.Services
{
    // Base image processor
    public interface IBaseImageProcessor
    {
        /// <summary>
        /// Re size an image
        /// </summary>
        /// <param name="request">Process image request</param>
        /// <param name="imageStream">Image stream</param>
        /// <returns>Image Process response</returns>
        Task<ImageProcessResponse> ResizeImage(ImageProcessRequest request, Stream imageStream);
    }
}