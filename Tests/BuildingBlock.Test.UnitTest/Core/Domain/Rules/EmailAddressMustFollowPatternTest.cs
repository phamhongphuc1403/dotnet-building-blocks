using BuildingBlock.Core.Domain.Rules.Implementations;
using FluentAssertions;

namespace Tests.Core.Domain.Rules;

public class EmailAddressMustFollowPatternTest
{
    public class ShouldReturnTrue
    {
        [Fact]
        public void WhenEmailIsEmpty()
        {
            // Arrange
            const string email = "";
            var emailAddressMustFollowPattern = new EmailAddressMustFollowPattern(email);

            // Act
            var result = emailAddressMustFollowPattern.IsBroken();

            // Assert
            result.Should().BeTrue();
            emailAddressMustFollowPattern.Message.Should().Be($"Email address: '{email}' can not be empty.");
        }

        public class WhenEmailIsInvalid
        {
            [Fact]
            public void Case1()
            {
                // Arrange
                const string email = "invalid";
                var emailAddressMustFollowPattern = new EmailAddressMustFollowPattern(email);

                // Act
                var result = emailAddressMustFollowPattern.IsBroken();

                // Assert
                result.Should().BeTrue();
                emailAddressMustFollowPattern.Message.Should().Be($"Email address: '{email}' is not valid.");
            }
        }
    }

    public class ShouldReturnFalse
    {
        public class WhenEmailIsValid
        {
            [Fact]
            public void Case1()
            {
                // Arrange
                var emailAddressMustFollowPattern = new EmailAddressMustFollowPattern("invalid@123");

                // Act
                var result = emailAddressMustFollowPattern.IsBroken();

                // Assert
                result.Should().BeFalse();
                emailAddressMustFollowPattern.Message.Should().BeNull();
            }

            [Fact]
            public void Case2()
            {
                // Arrange
                var emailAddressMustFollowPattern = new EmailAddressMustFollowPattern("invalid@abc.def");

                // Act
                var result = emailAddressMustFollowPattern.IsBroken();

                // Assert
                result.Should().BeFalse();
                emailAddressMustFollowPattern.Message.Should().BeNull();
            }
        }
    }
}