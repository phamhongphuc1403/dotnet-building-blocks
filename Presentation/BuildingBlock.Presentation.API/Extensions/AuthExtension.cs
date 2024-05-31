using BuildingBlock.Core.Application;
using BuildingBlock.Presentation.API.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlock.Presentation.API.Extensions;

public static class AuthExtension
{
    public static IServiceCollection AddCurrentUser(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();

        services.AddScoped<ICurrentUser, CurrentUser>();

        return services;
    }
}