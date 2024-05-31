using Microsoft.Extensions.Configuration;

namespace BuildingBlock.Core.Domain.Shared.Utils;

public static class ConfigurationUtilities
{
    public static string GetRequiredValue(this IConfiguration configuration, string name)
    {
        if (string.IsNullOrEmpty(configuration[name]))
            throw new InvalidOperationException(
                $"Configuration missing value for: {(configuration is IConfigurationSection s ? s.Path + ": " + name : name)}");

        return configuration[name]!;
    }

    public static string GetRequiredConnectionString(this IConfiguration configuration, string name)
    {
        return configuration.GetConnectionString(name) ?? throw new InvalidOperationException(
            $"Configuration missing value for: {(configuration is IConfigurationSection s ? s.Path + ":ConnectionStrings: " + name : "ConnectionStrings: " + name)}");
    }

    public static T BindAndGetConfig<T>(this IConfiguration configuration, string sectionName)
    {
        var config = configuration.GetSection(sectionName).Get<T>();

        if (config == null) throw new Exception($"{sectionName} configuration is not provided.");

        CheckForNullProperties(config, sectionName);

        return config;
    }

    private static void CheckForNullProperties(object obj, string sectionName)
    {
        var properties = obj.GetType().GetProperties();

        foreach (var property in properties)
        {
            var value = property.GetValue(obj, null);

            if (value == null)
                throw new Exception($"Property '{property.Name}' in section '{sectionName}' is missing.");
        }
    }
}