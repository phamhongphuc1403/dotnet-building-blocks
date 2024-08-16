using BuildingBlock.Core.Domain.Exceptions;
using BuildingBlock.Core.Domain.Rules.Implementations;
using BuildingBlock.Core.Domain.ValueObjects.Abstractions;
using BuildingBlock.Core.Domain.ValueObjects.Implementations;
using FluentAssertions;

namespace Tests.Core.Domain.ValueObjects.Abstractions;

public class ValueObjectTest
{
    public class Equals
    {
        public class ShouldReturnTrue
        {
            [Fact]
            public void WhenTwoValueObjectsAreEqual()
            {
                // Arrange
                var valueObject1 = new EmailAddress("string@123");
                var valueObject2 = new EmailAddress("string@123");

                // Act
                var actualData = valueObject1.Equals(valueObject2);

                // Assert
                actualData.Should().BeTrue();
            }
        }

        public class ShouldReturnFalse
        {
            [Fact]
            public void WhenTwoValueObjectsAreNotEqual()
            {
                // Arrange
                var valueObject1 = new EmailAddress("string@123");
                var valueObject2 = new EmailAddress("string2@123");

                // Act
                var actualData = valueObject1.Equals(valueObject2);

                // Assert
                actualData.Should().BeFalse();
            }
        }
    }

    public class CheckRule
    {
        public class ShouldThrowValidationException
        {
            [Fact]
            public void WhenRuleIsBroken()
            {
                // Arrange
                const string name = "string";
                const string value = "";
                var rule = new StringMustNotBeEmptyOrWhiteSpace(value, name);

                // Act
                var action = () => ValueObject.CheckRule(rule);

                // Assert
                action.Should().Throw<ValidationException>();
                rule.Message.Should().Be($"{name} with value: {value} can not be empty or contain only white spaces.");
            }
        }
    }

    public class ShouldDoNothing
    {
        public class WhenRuleIsNotBroken
        {
            [Fact]
            public void ShouldDoNothingWhenRuleIsNotBroken()
            {
                // Arrange
                var rule = new StringMustNotBeEmptyOrWhiteSpace("string", "string");

                // Act
                var action = new Action(() => ValueObject.CheckRule(rule));

                // Assert
                action.Should().NotThrow<ValidationException>();
                rule.Message.Should().BeNull();
            }
        }
    }
}