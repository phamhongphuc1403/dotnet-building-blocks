using BuildingBlock.Core.Domain.Shared.Utils;
using BuildingBlock.Core.Domain.ValueObjects.Implementations;
using FluentAssertions;

namespace Tests.Core.Domain.ValueObjects;

public class ObjectValueUtilityTest
{
    public class ToNormalizedName
    {
        public class ShouldNormalizeClassName
        {
            [Fact]
            public void Case1()
            {
                // Arrange
                var mockValueObject = new EmailAddress("test@123");

                // Act
                var normalizedName = mockValueObject.ToNormalizedName();

                // Assert
                normalizedName.Should().Be("email address");
            }
        }
    }
}