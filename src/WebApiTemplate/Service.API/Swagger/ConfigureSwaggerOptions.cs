using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Service.API.Swagger
{
    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        readonly IApiVersionDescriptionProvider _apiVerProvider;

        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider apiVerProvider) => _apiVerProvider = apiVerProvider;
        
        public void Configure(SwaggerGenOptions options)
        {
            foreach (var description in _apiVerProvider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, GetSwaggerDocInfo(description));
            }
        }       

        static OpenApiInfo GetSwaggerDocInfo(ApiVersionDescription description)
        {
            var info = new OpenApiInfo
            {
                Title = $"WebAPI {description.ApiVersion}",
                Version = description.GroupName,
                Description = "Web API Template",
                Contact = new OpenApiContact()
                {
                    Name = "Web API service"
                },
                License = new OpenApiLicense()
                {
                    Name = "MIT"
                }
            };

            if (description.IsDeprecated)
            {
                info.Description += $" {description.ApiVersion} API version is deprecated.";
            }

            return info;
        }
    }
}
