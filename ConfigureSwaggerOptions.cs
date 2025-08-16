using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.OpenApi.Models;

public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
{
    private readonly IApiVersionDescriptionProvider _provider;

    public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider)
    {
        _provider = provider;
    }

    public void Configure(SwaggerGenOptions options)
    {
        foreach (var description in _provider.ApiVersionDescriptions)
        {
            options.SwaggerDoc(description.GroupName,
                new OpenApiInfo
                {
                    Title = "Task Management API",
                    Version = description.ApiVersion.ToString(),
                    Description = description.GroupName == "v1"
                        ? "Version 1 - Basic API for managing daily tasks"
                        : "Version 2 - Improved API with alerts and extra features",
                    Contact = new OpenApiContact
                    {
                        Name = "AROCKIA DIVYA A",
                        Email = "msvijaya98@gmail.com",
                        Url = new Uri("https://github.com/aro26")
                    }
                });
        }
    }
}
