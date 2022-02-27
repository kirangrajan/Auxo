using Auxo.Prototype.Services;

using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(Auxo.AzureFunctions.Startup))]

namespace Auxo.AzureFunctions
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            this.ConfigureServices(builder.Services);
        }

        private void ConfigureServices(IServiceCollection builderServices)
        {
            builderServices.AddScoped<IImageProcessService, ImageProcessService>();
            builderServices.AddScoped<IGenericImageProcessor, GenericImageProcessor>();
        }
    }
}