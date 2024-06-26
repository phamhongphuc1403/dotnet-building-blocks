using BuildingBlock.Core.Application.Services.EmailService;
using BuildingBlock.Core.Domain.Shared.Utils;
using BuildingBlock.Infrastructure.Mailkit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BuildingBlock.Presentation.API.Extensions;

public static class EmailExtension
{
    public static IServiceCollection AddEmail(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IEmailService, MailkitEmailService>(sp =>
        {
            var emailConfiguration = configuration.BindAndGetConfig<EmailConfiguration>("Email");

            var logger = sp.GetRequiredService<ILogger<MailkitEmailService>>();

            return new MailkitEmailService(emailConfiguration, logger);
        });

        return services;
    }
}