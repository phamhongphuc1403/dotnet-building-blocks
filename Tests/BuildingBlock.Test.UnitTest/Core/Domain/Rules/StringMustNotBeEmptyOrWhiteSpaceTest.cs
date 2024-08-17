using BuildingBlock.Core.Domain.Rules.Implementations;
using FluentAssertions;

namespace Tests.Core.Domain.Rules;

public class StringMustNotBeEmptyOrWhiteSpaceTest
{
    public class ShouldReturnTrue
    {
        [Fact]
        public void WhenStringIsEmpty()
        {
            // Arrange
            var stringCanNotBeEmptyOrWhiteSpaceRule = new StringMustNotBeEmptyOrWhiteSpace(string.Empty, "string");

            // Act
            var result = stringCanNotBeEmptyOrWhiteSpaceRule.IsBroken();

            // Assert
            result.Should().BeTrue();
            stringCanNotBeEmptyOrWhiteSpaceRule.Message.Should()
                .Be("string with value: '' can not be empty or contain only white spaces.");
        }

        [Fact]
        public void WhenStringIsNull()
        {
            // Arrange
            var stringCanNotBeEmptyOrWhiteSpaceRule = new StringMustNotBeEmptyOrWhiteSpace(null, "string");

            // Act
            var result = stringCanNotBeEmptyOrWhiteSpaceRule.IsBroken();

            // Assert
            result.Should().BeTrue();
            stringCanNotBeEmptyOrWhiteSpaceRule.Message.Should()
                .Be("string with value: '' can not be empty or contain only white spaces.");
        }

        [Fact]
        public void WhenStringContainsOnlyWhiteSpaces()
        {
            // Arrange
            var stringCanNotBeEmptyOrWhiteSpaceRule = new StringMustNotBeEmptyOrWhiteSpace("        ", "string");

            // Act
            var result = stringCanNotBeEmptyOrWhiteSpaceRule.IsBroken();

            // Assert
            result.Should().BeTrue();
            stringCanNotBeEmptyOrWhiteSpaceRule.Message.Should()
                .Be("string with value: '        ' can not be empty or contain only white spaces.");
        }
    }

    public class ShouldReturnFalse
    {
        [Fact]
        public void WhenStringContainsCharacters()
        {
            // Arrange
            var stringCanNotBeEmptyOrWhiteSpaceRule = new StringMustNotBeEmptyOrWhiteSpace("test", "string");

            // Act
            var result = stringCanNotBeEmptyOrWhiteSpaceRule.IsBroken();

            // Assert
            result.Should().BeFalse();
            stringCanNotBeEmptyOrWhiteSpaceRule.Message.Should().BeNull();
        }
    }
}