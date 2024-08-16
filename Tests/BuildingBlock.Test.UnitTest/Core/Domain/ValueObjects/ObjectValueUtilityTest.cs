using BuildingBlock.Core.Domain.ValueObjects;
using BuildingBlock.Core.Domain.ValueObjects.Abstractions;
using FluentAssertions;

namespace Tests.Core.Domain.ValueObjects;

public class ObjectValueUtilityTest
{
    public class ToNormalizedName
    {
        public class TestValueObject : ValueObject
        {
            public string ActualData;

            public TestValueObject()
            {
                ActualData = this.ToNormalizedName();
            }

            protected override IEnumerable<object?> GetValues()
            {
                yield return ActualData;
            }
        }

        public class ShouldNormalizeClassName
        {
            [Fact]
            public void Case1()
            {
                // Arrange
                var mockValueObject = new TestValueObject();

                // Act
                var normalizedName = mockValueObject.ToNormalizedName();

                // Assert
                normalizedName.Should().Be("test value object");
            }
        }
    }
}