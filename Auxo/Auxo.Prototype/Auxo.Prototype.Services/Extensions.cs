using System;
using System.Globalization;

namespace Auxo.Prototype.Services
{
    /// <summary>
    /// Common Extensions
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Convert a string to an integer
        /// </summary>
        /// <param name="val">string to be converted to an integer</param>
        /// <returns>integer value</returns>
        public static int ToInt(this string val)
        {
            int result;

            return !string.IsNullOrEmpty(val) ? (int.TryParse(val, NumberStyles.Number, CultureInfo.CurrentUICulture, out result) ? result : 0) : 0;
        }    
        
        /// <summary>
        /// Convert an double to an integer
        /// </summary>
        /// <param name="val">Object to be converted to an integer</param>
        /// <returns>Object converted to integer</returns>
        public static int ToInt(this double val)
        {
            return Convert.ToInt32(val);
        }
    }
}