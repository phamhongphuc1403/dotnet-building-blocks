using BuildingBlock.Core.Domain.Shared.Services;
using BuildingBlock.Core.Domain.Shared.Utils;
using BuildingBlock.Infrastructure.EntityFrameworkCore;
using BuildingBlock.Presentation.API.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlock.Presentation.API.Extensions;

public static class DatabaseExtension
{
    public static IServiceCollection AddDatabase<TDbContext>(this IServiceCollection services,
        IConfiguration configuration) where TDbContext : DbContext
    {
        var databaseConfiguration = configuration.BindAndGetConfig<DatabaseConfiguration>("Database");

        services.AddDbContext<TDbContext>(options =>
        {
            options.UseNpgsql(databaseConfiguration.ToString(),
                o => { o.MigrationsHistoryTable(HistoryRepository.DefaultTableName, typeof(TDbContext).Name); });
            options.EnableSensitiveDataLogging();
            options.UseSnakeCaseNamingConvention();
        });

        services.AddScoped<Func<DbContext>>(provider => () => provider.GetService<TDbContext>()!);
        services.AddScoped<IUnitOfWork, UnitOfWork<TDbContext>>();

        return services;
    }

    public static async Task ApplyMigrationAsync<TDbContext>(this IServiceCollection services)
        where TDbContext : DbContext
    {
        using var scope = services.BuildServiceProvider().CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<TDbContext>();
        if ((await context.Database.GetPendingMigrationsAsync()).Any()) await context.Database.MigrateAsync();
    }
}