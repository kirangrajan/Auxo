namespace Auxo.Prototype.DomainModel
{
    public class ImageProcessRequest
    {
        /// <summary>
        /// Minimum Height
        /// </summary>
        public double MinimumHeight { get; set; } 
        
        /// <summary>
        /// Minimum Width
        /// </summary>
        public double MinimumWidth { get; set; }

        /// <summary>
        /// Minimum Width
        /// </summary>
        public bool CompressImage { get; set; }

        /// <summary>
        /// File Name
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// File Extension
        /// </summary>
        public string FileExtension { get; set; }

        /// <summary>
        /// Maximum Height
        /// </summary>
        public double MaximumHeight => 1000;

        /// <summary>
        /// Maximum Width
        /// </summary>
        public double MaximumWidth => 1000;
    }
}
