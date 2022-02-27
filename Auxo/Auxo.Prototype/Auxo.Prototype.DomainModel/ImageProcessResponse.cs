namespace Auxo.Prototype.DomainModel
{
    /// <summary>
    /// Image process response
    /// </summary>
    public class ImageProcessResponse
    {
        /// <summary>
        /// Processing Status
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// File Location
        /// </summary>
        public string FileLocation { get; set; }

        /// <summary>
        /// New file name
        /// </summary>
        public string Filename { get; set; }

        /// <summary>
        /// Failure Reason
        /// </summary>
        public string FailureReason { get; set; }

        /// <summary>
        /// File height
        /// </summary>
        public int FileHeight { get; set; }

        /// <summary>
        /// New file width
        /// </summary>
        public int FileWidth { get; set; }
    }
}
