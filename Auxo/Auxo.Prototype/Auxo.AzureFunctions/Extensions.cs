using System.Buffers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;

using Newtonsoft.Json;

namespace Auxo.AzureFunctions
{
    /// <summary>
    /// Common extensions
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Converts an object response to camel case
        /// </summary>
        /// <param name="response">Response object</param>
        /// <returns>Response object converted to camel case</returns>
        public static ObjectResult ToCamelCase(this ObjectResult response)
        {
            var settings = JsonSerializerSettingsProvider.CreateSerializerSettings();
            settings.ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();
            settings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;

            var formatter = new NewtonsoftJsonOutputFormatter(
                settings,
                ArrayPool<char>.Shared,
                new MvcOptions { AllowEmptyInputInBodyModelBinding = true });

            response.Formatters.Add(formatter);
            return response;
        }
    }
}