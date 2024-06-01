using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlock.Presentation.API.Extensions;

public static class RealTimeExtension
{
    public static IServiceCollection AddRealTime(this IServiceCollection services)
    {
        services.AddSignalR();

        return services;
    }
}