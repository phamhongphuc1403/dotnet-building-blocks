using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.SignalR;

namespace BuildingBlock.Presentation.API.Middlewares;

public static class RealTimeMiddleware
{
    public static IEndpointRouteBuilder UseRealTime(this IEndpointRouteBuilder app)
    {
        app.MapHub<Hub>("hub");

        return app;
    }
}