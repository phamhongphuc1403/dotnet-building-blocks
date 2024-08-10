using BuildingBlock.Core.Domain.Shared.Utils;
using FluentAssertions;
using Microsoft.Extensions.Configuration;

namespace Tests.Core.Domain.Shared.Utils;

public class ConfigurationUtilitiesTest
{
    public class GetRequiredValue
    {
        private const string Name = "Name";

        public class ShouldThrowInvalidOperationException
        {
            [Fact]
            public void WhenConfigurationValueIsEmpty()
            {
                // Arrange
                var configuration = new ConfigurationBuilder().Build();

                // Act
                var exception = Assert.Throws<InvalidOperationException>(() => configuration.GetRequiredValue(Name));

                // Assert
                Assert.Equal($"Configuration missing value for: {Name}", exception.Message);
            }
        }

        public class ShouldReturnValue
        {
            [Fact]
            public void WhenConfigurationValueIsNotEmpty()
            {
                // Arrange
                const string value = "Value";
                var configuration = new ConfigurationBuilder()
                    .AddInMemoryCollection(new Dictionary<string, string> { { Name, value } }!).Build();

                // Act
                var result = configuration.GetRequiredValue(Name);

                // Assert
                Assert.Equal(value, result);
            }
        }
    }

    public class GetRequiredConnectionString
    {
        private const string Value = "Value";
        private const string Name = "Name";


        public class ShouldThrowInvalidOperationException
        {
            [Fact]
            public void WhenConfigurationDontHaveConnectionStringsSection()
            {
                // Arrange
                var configuration = new ConfigurationBuilder().Build();

                // Act
                var exception =
                    Assert.Throws<InvalidOperationException>(() => configuration.GetRequiredConnectionString(Name));

                // Assert
                Assert.Equal($"Configuration missing value for: ConnectionStrings: {Name}", exception.Message);
            }

            [Fact]
            public void WhenNameDoesntExistInConnectionStringsSection()
            {
                // Arrange
                var inMemorySettings = new Dictionary<string, string>
                {
                    { "ConnectionStrings:ABC", "Value" }
                };

                var configuration = new ConfigurationBuilder()
                    .AddInMemoryCollection(inMemorySettings!)
                    .Build();

                // Act
                var exception =
                    Assert.Throws<InvalidOperationException>(() => configuration.GetRequiredConnectionString(Name));

                // Assert
                Assert.Equal($"Configuration missing value for: ConnectionStrings: {Name}", exception.Message);
            }
        }

        public class ShouldReturnValue
        {
            [Fact]
            public void WhenNameExistInConnectionStringsSection()
            {
                // Arrange
                var inMemorySettings = new Dictionary<string, string>
                {
                    { "ConnectionStrings:Name", "Value" }
                };

                var configuration = new ConfigurationBuilder()
                    .AddInMemoryCollection(inMemorySettings!)
                    .Build();

                // Act
                var result = configuration.GetRequiredConnectionString(Name);

                // Assert
                Assert.Equal(Value, result);
            }
        }
    }

    public class BindAndGetConfig
    {
        private const string SectionName = "Jwt";

        private class JwtConfiguration
        {
            public string Audience { get; set; } = null!;

            public string Issuer { get; set; } = null!;

            public string AccessTokenSecurityKey { get; set; } = null!;

            public int AccessTokenLifeTimeInMinute { get; set; }

            public string RefreshTokenSecurityKey { get; set; } = null!;

            public int RefreshTokenLifeTimeInMinute { get; set; }
        }

        public class ShouldReturnException
        {
            [Fact]
            public void WhenSectionIsNotProvided()
            {
                // Arrange
                var configuration = new ConfigurationBuilder().Build();

                // Act
                Action act = () => configuration.BindAndGetConfig<JwtConfiguration>(SectionName);

                // Assert
                act.Should().Throw<Exception>().WithMessage($"{SectionName} configuration is not provided.");
            }
        }

        public class ShouldReturnConfiguration
        {
            [Fact]
            public void WhenSectionIsProvided()
            {
                var jwtConfiguration = new JwtConfiguration
                {
                    Audience = "Audience",
                    Issuer = "Issuer",
                    AccessTokenSecurityKey = "AccessTokenSecurityKey",
                    AccessTokenLifeTimeInMinute = 1,
                    RefreshTokenSecurityKey = "RefreshTokenSecurityKey",
                    RefreshTokenLifeTimeInMinute = 1
                };

                var configuration = new ConfigurationBuilder()
                    .AddInMemoryCollection(new Dictionary<string, string>
                    {
                        { $"{SectionName}:Audience", jwtConfiguration.Audience },
                        { $"{SectionName}:Issuer", jwtConfiguration.Issuer },
                        { $"{SectionName}:AccessTokenSecurityKey", jwtConfiguration.AccessTokenSecurityKey },
                        {
                            $"{SectionName}:AccessTokenLifeTimeInMinute",
                            jwtConfiguration.AccessTokenLifeTimeInMinute.ToString()
                        },
                        { $"{SectionName}:RefreshTokenSecurityKey", jwtConfiguration.RefreshTokenSecurityKey },
                        {
                            $"{SectionName}:RefreshTokenLifeTimeInMinute",
                            jwtConfiguration.RefreshTokenLifeTimeInMinute.ToString()
                        }
                    }!).Build();

                // Act
                var result = configuration.BindAndGetConfig<JwtConfiguration>(SectionName);

                // Assert
                result.Should().BeEquivalentTo(jwtConfiguration);
            }
        }
    }
}