using System.Text.Json.Serialization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlock.Presentation.API.Extensions;

public static class DefaultExtensions
{
    public static IServiceCollection AddDefaultExtensions(this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

        services
            .AddApplicationCors(configuration)
            .AddHttpContextAccessor()
            .AddCurrentUser()
            .AddDefaultOpenApi(configuration)
            .AddEventBus(configuration)
            .AddInMemoryCache(configuration)
            .AddEmail(configuration);

        return services;
    }
}