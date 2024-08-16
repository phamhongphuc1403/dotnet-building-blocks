using BuildingBlock.Core.Domain.Rules.Implementations;
using FluentAssertions;

namespace Tests.Core.Domain.Rules;

public class StringCanNotBeEmptyOrWhiteSpaceRuleTest
{
    private const string Name = "String";

    public class ShouldReturnTrue
    {
        [Fact]
        public void WhenStringIsEmpty()
        {
            // Arrange
            var value = string.Empty;
            var stringCanNotBeEmptyOrWhiteSpaceRule = new StringCanNotBeEmptyOrWhiteSpaceRule(value, Name);

            // Act
            var result = stringCanNotBeEmptyOrWhiteSpaceRule.IsBroken();

            // Assert
            result.Should().BeTrue();
            stringCanNotBeEmptyOrWhiteSpaceRule.Message.Should()
                .Be($"{Name} with value: {value} can not be empty or contain only white spaces.");
        }

        [Fact]
        public void WhenStringIsNull()
        {
            // Arrange
            string? value = null;
            var stringCanNotBeEmptyOrWhiteSpaceRule = new StringCanNotBeEmptyOrWhiteSpaceRule(value, Name);

            // Act
            var result = stringCanNotBeEmptyOrWhiteSpaceRule.IsBroken();

            // Assert
            result.Should().BeTrue();
            stringCanNotBeEmptyOrWhiteSpaceRule.Message.Should()
                .Be($"{Name} with value: {value} can not be empty or contain only white spaces.");
        }

        [Fact]
        public void WhenStringContainsOnlyWhiteSpaces()
        {
            // Arrange
            var value = "        ";
            var stringCanNotBeEmptyOrWhiteSpaceRule = new StringCanNotBeEmptyOrWhiteSpaceRule(value, Name);

            // Act
            var result = stringCanNotBeEmptyOrWhiteSpaceRule.IsBroken();

            // Assert
            result.Should().BeTrue();
            stringCanNotBeEmptyOrWhiteSpaceRule.Message.Should()
                .Be($"{Name} with value: {value} can not be empty or contain only white spaces.");
        }
    }

    public class ShouldReturnFalse
    {
        [Fact]
        public void WhenStringContainsCharacters()
        {
            // Arrange
            var value = "test";
            var stringCanNotBeEmptyOrWhiteSpaceRule = new StringCanNotBeEmptyOrWhiteSpaceRule(value, Name);

            // Act
            var result = stringCanNotBeEmptyOrWhiteSpaceRule.IsBroken();

            // Assert
            result.Should().BeFalse();
            stringCanNotBeEmptyOrWhiteSpaceRule.Message.Should().BeNull();
        }
    }
}