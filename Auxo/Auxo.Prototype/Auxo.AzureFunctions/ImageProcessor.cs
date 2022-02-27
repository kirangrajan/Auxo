using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

using Auxo.Prototype.DomainModel;
using Auxo.Prototype.Services;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

using Newtonsoft.Json;

namespace Auxo.AzureFunctions
{
    public class ImageProcessor
    {
        private const string Success = "SUCCESS";

        private static readonly string[] SupportedExtensions = { ".png", ".jpg", ".jpeg" };

        private readonly IImageProcessService _imageProcessService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageProcessor"/> class. 
        /// </summary>
        /// <param name="imageProcessService">
        /// The image Process Service.
        /// </param>
        public ImageProcessor(IImageProcessService imageProcessService)
        {
            this._imageProcessService = imageProcessService;
        }

        [FunctionName("ResizeImage")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "POST", Route = "ImageProcess/Resize")]
            HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Image processing triggered.");

            try
            {
                string name = req.Query["name"];

                var fileToPost = req.Form.Files["fileToResize"];
                var fileExtension = Path.GetExtension(fileToPost.FileName);

                log.LogInformation($"File to resize - {fileToPost}{fileExtension}");

                if (!SupportedExtensions.Contains(fileExtension))
                {
                    return new BadRequestObjectResult(
                        new
                            {
                                Status = "Failure",
                                StatusCode = HttpStatusCode.BadRequest,
                                Error = "Unsupported file extension"
                            }).ToCamelCase();
                }

                if (fileToPost.Length <= 0)
                {
                    return new BadRequestObjectResult(
                            new { Status = "Failure", StatusCode = HttpStatusCode.BadRequest, Error = "Invalid file" })
                        .ToCamelCase();
                }

                var ms = new MemoryStream();
                fileToPost.CopyTo(ms);

                var result = await this._imageProcessService.ProcessImage(
                                 new ImageProcessRequest
                                     {
                                         FileExtension = fileExtension,
                                         FileName = fileToPost.FileName,
                                         CompressImage = true
                                     },
                                 ms);
                return string.Equals(result.Status, Success, StringComparison.CurrentCultureIgnoreCase)
                           ? new OkObjectResult(
                               new
                                   {
                                       StatusCode = HttpStatusCode.OK,
                                       Message = "Image resized successfully!!",
                                       result.FileLocation,
                                       result.Filename
                                   }).ToCamelCase()
                           : new BadRequestObjectResult(
                               new
                                   {
                                       Status = "Failure",
                                       result.FailureReason,
                                       StatusCode = HttpStatusCode.BadRequest,
                                       Error = "Image resize failed!!"
                                   }).ToCamelCase();
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(
                    new
                        {
                            Status = "Failure",
                            StatusCode = HttpStatusCode.BadRequest,
                            Error = $"Error while processing {JsonConvert.SerializeObject(ex)}"
                        }).ToCamelCase();
            }
        }
    }
}
