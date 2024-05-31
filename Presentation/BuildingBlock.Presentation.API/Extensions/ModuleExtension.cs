using BuildingBlock.Core.Application;
using BuildingBlock.Core.Domain;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlock.Presentation.API.Extensions;

public static class ModuleExtension
{
    public static async Task<IServiceCollection> AddDefaultModuleExtensionsAsync<TApplicationAssemblyReference,
        TDomainAssemblyReference,
        TDbContext>(this IServiceCollection services,
        IConfiguration configuration) where TDbContext : DbContext
        where TApplicationAssemblyReference : ApplicationAssemblyReference
        where TDomainAssemblyReference : DomainAssemblyReference
    {
        services
            .AddDatabase<TDbContext>(configuration)
            .AddMapper<TDbContext>()
            .AddCqrs<TApplicationAssemblyReference, TDomainAssemblyReference>()
            .AddValidatorsFromAssembly(typeof(TApplicationAssemblyReference).Assembly);

        services
            .RegisterSeeders<TApplicationAssemblyReference>()
            .RegisterServices<TApplicationAssemblyReference>()
            .RegisterServices<TDomainAssemblyReference>()
            .RegisterDefaultRepositories<TDomainAssemblyReference, TDbContext>()
            .RegisterCustomRepositories<TDbContext>()
            .RegisterCachedRepositories<TDbContext>()
            .RegisterIntegrationEventHandlers<TApplicationAssemblyReference>()
            .RegisterUnitOfWork<TDbContext>();

        await services.ApplyMigrationAsync<TDbContext>();

        return services;
    }
}