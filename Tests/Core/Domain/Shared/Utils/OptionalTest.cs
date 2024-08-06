using BuildingBlock.Core.Domain.Shared.Utils;
using FluentAssertions;

namespace Tests.Core.Domain.Shared.Utils;

public class OptionalTest
{
    public class ThrowIfExist
    {
        public class ShouldThrowException
        {
            [Fact]
            public void WhenInstanceIsNotNull()
            {
                // Arrange
                var optional = Optional<object>.Of(new object());
                const string exceptionMessage = "123";

                // Act
                Action act = () => optional.ThrowIfExist(new Exception(exceptionMessage));

                // Assert
                act.Should().Throw<Exception>().WithMessage(exceptionMessage);
            }

            [Fact]
            public void WhenInstanceIsTrue()
            {
                // Arrange
                var optional = Optional<bool>.Of(true);
                const string exceptionMessage = "123";

                // Act
                Action act = () => optional.ThrowIfExist(new Exception(exceptionMessage));

                // Assert
                act.Should().Throw<Exception>().WithMessage(exceptionMessage);
            }
        }

        public class ShouldReturnInstance
        {
            [Fact]
            public void WhenInstanceIsNull()
            {
                // Arrange
                var optional = Optional<object>.Of(null);

                // Act
                var result = optional.ThrowIfExist(new Exception());

                // Assert
                result.Should().BeSameAs(optional);
            }

            [Fact]
            public void WhenInstanceIsFalse()
            {
                // Arrange
                var optional = Optional<bool>.Of(false);

                // Act
                var result = optional.ThrowIfExist(new Exception());

                // Assert
                result.Should().BeSameAs(optional);
            }
        }
    }

    public class ThrowIfNotExist
    {
        public class ShouldThrowException
        {
            [Fact]
            public void WhenInstanceIsNull()
            {
                // Arrange
                var optional = Optional<object>.Of(null);
                const string exceptionMessage = "123";

                // Act
                Action act = () => optional.ThrowIfNotExist(new Exception(exceptionMessage));

                // Assert
                act.Should().Throw<Exception>().WithMessage(exceptionMessage);
            }

            [Fact]
            public void WhenInstanceIsFalse()
            {
                // Arrange
                var optional = Optional<bool>.Of(false);
                const string exceptionMessage = "123";

                // Act
                Action act = () => optional.ThrowIfNotExist(new Exception(exceptionMessage));

                // Assert
                act.Should().Throw<Exception>().WithMessage(exceptionMessage);
            }
        }

        public class ShouldReturnInstance
        {
            [Fact]
            public void WhenInstanceIsNotNull()
            {
                // Arrange
                var optional = Optional<object>.Of(new object());

                // Act
                var result = optional.ThrowIfNotExist(new Exception());

                // Assert
                result.Should().BeSameAs(optional);
            }

            [Fact]
            public void WhenInstanceIsTrue()
            {
                // Arrange
                var optional = Optional<bool>.Of(true);

                // Act
                var result = optional.ThrowIfNotExist(new Exception());

                // Assert
                result.Should().BeSameAs(optional);
            }
        }
    }

    public class ThrowIfEqual
    {
        public class ShouldThrowException
        {
            public class WhenInstanceIsEqual
            {
                [Fact]
                public void InstanceIsBoolean()
                {
                    // Arrange
                    var optional = Optional<bool>.Of(true);
                    const string exceptionMessage = "123";

                    // Act
                    Action act = () => optional.ThrowIfEqual(true, new Exception(exceptionMessage));

                    // Assert
                    act.Should().Throw<Exception>().WithMessage(exceptionMessage);
                }

                [Fact]
                public void InstanceIsNumber()
                {
                    // Arrange
                    var optional = Optional<int>.Of(1);
                    const string exceptionMessage = "123";

                    // Act
                    Action act = () => optional.ThrowIfEqual(1, new Exception(exceptionMessage));

                    // Assert
                    act.Should().Throw<Exception>().WithMessage(exceptionMessage);
                }

                [Fact]
                public void InstanceIsString()
                {
                    // Arrange
                    var optional = Optional<string>.Of("test");
                    const string exceptionMessage = "123";

                    // Act
                    Action act = () => optional.ThrowIfEqual("test", new Exception(exceptionMessage));

                    // Assert
                    act.Should().Throw<Exception>().WithMessage(exceptionMessage);
                }

                [Fact]
                public void InstanceIsObject()
                {
                    // Arrange
                    var optional = Optional<object>.Of(new { Name = "test", Description = "description" });
                    const string exceptionMessage = "123";

                    // Act
                    Action act = () => optional.ThrowIfEqual(new { Name = "test", Description = "description" },
                        new Exception(exceptionMessage));

                    // Assert
                    act.Should().Throw<Exception>().WithMessage(exceptionMessage);
                }

                [Fact]
                public void InstanceIsObjectWithNull()
                {
                    // Arrange
                    var optional = Optional<object>.Of(null);
                    const string exceptionMessage = "123";

                    // Act
                    Action act = () => optional.ThrowIfEqual(null, new Exception(exceptionMessage));

                    // Assert
                    act.Should().Throw<Exception>().WithMessage(exceptionMessage);
                }
            }

            public class ShouldReturnInstance
            {
                public class WhenInstanceIsNotEqual
                {
                    [Fact]
                    public void InstanceIsBoolean()
                    {
                        // Arrange
                        var optional = Optional<bool>.Of(true);

                        // Act
                        var result = optional.ThrowIfEqual(false, new Exception());

                        // Assert
                        result.Should().BeSameAs(optional);
                    }

                    [Fact]
                    public void InstanceIsNumber()
                    {
                        // Arrange
                        var optional = Optional<int>.Of(1);

                        // Act
                        var result = optional.ThrowIfEqual(2, new Exception());

                        // Assert
                        result.Should().BeSameAs(optional);
                    }

                    [Fact]
                    public void InstanceIsString()
                    {
                        // Arrange
                        var optional = Optional<string>.Of("test");

                        // Act
                        var result = optional.ThrowIfEqual("test1", new Exception());

                        // Assert
                        result.Should().BeSameAs(optional);
                    }

                    [Fact]
                    public void InstanceIsObject()
                    {
                        // Arrange
                        var optional = Optional<object>.Of(new { Name = "test", Description = "description" });

                        // Act
                        var result = optional.ThrowIfEqual(new { Name = "test1", Description = "description" },
                            new Exception());

                        // Assert
                        result.Should().BeSameAs(optional);
                    }

                    [Fact]
                    public void InstanceIsObjectWithNull()
                    {
                        // Arrange
                        var optional = Optional<object>.Of(null);

                        // Act
                        var result = optional.ThrowIfEqual(new { Name = "test1", Description = "description" },
                            new Exception());

                        // Assert
                        result.Should().BeSameAs(optional);
                    }
                }
            }
        }
    }

    public class ThrowIfNotEqual
    {
        public class ShouldThrowException
        {
            public class WhenInstanceIsNotEqual
            {
                [Fact]
                public void InstanceIsBoolean()
                {
                    // Arrange
                    var optional = Optional<bool>.Of(true);
                    const string exceptionMessage = "123";

                    // Act
                    Action act = () => optional.ThrowIfNotEqual(false, new Exception(exceptionMessage));

                    // Assert
                    act.Should().Throw<Exception>().WithMessage(exceptionMessage);
                }

                [Fact]
                public void InstanceIsNumber()
                {
                    // Arrange
                    var optional = Optional<int>.Of(1);
                    const string exceptionMessage = "123";

                    // Act
                    Action act = () => optional.ThrowIfNotEqual(2, new Exception(exceptionMessage));

                    // Assert
                    act.Should().Throw<Exception>().WithMessage(exceptionMessage);
                }

                [Fact]
                public void InstanceIsString()
                {
                    // Arrange
                    var optional = Optional<string>.Of("test");
                    const string exceptionMessage = "123";

                    // Act
                    Action act = () => optional.ThrowIfNotEqual("test1", new Exception(exceptionMessage));

                    // Assert
                    act.Should().Throw<Exception>().WithMessage(exceptionMessage);
                }

                [Fact]
                public void InstanceIsObject()
                {
                    // Arrange
                    var optional = Optional<object>.Of(new { Name = "test", Description = "description" });
                    const string exceptionMessage = "123";

                    // Act
                    Action act = () => optional.ThrowIfNotEqual(new { Name = "test1", Description = "description" },
                        new Exception(exceptionMessage));

                    // Assert
                    act.Should().Throw<Exception>().WithMessage(exceptionMessage);
                }

                [Fact]
                public void InstanceIsObjectWithNull()
                {
                    // Arrange
                    var optional = Optional<object>.Of(null);
                    const string exceptionMessage = "123";

                    // Act
                    Action act = () => optional.ThrowIfNotEqual(new { Name = "test1", Description = "description" },
                        new Exception(exceptionMessage));

                    // Assert
                    act.Should().Throw<Exception>().WithMessage(exceptionMessage);
                }
            }
        }

        public class ShouldReturnInstance
        {
            public class WhenInstanceIsEqual
            {
                [Fact]
                public void InstanceIsBoolean()
                {
                    // Arrange
                    var optional = Optional<bool>.Of(true);

                    // Act
                    var result = optional.ThrowIfNotEqual(true, new Exception());

                    // Assert
                    result.Should().BeSameAs(optional);
                }

                [Fact]
                public void InstanceIsNumber()
                {
                    // Arrange
                    var optional = Optional<int>.Of(1);

                    // Act
                    var result = optional.ThrowIfNotEqual(1, new Exception());

                    // Assert
                    result.Should().BeSameAs(optional);
                }

                [Fact]
                public void InstanceIsString()
                {
                    // Arrange
                    var optional = Optional<string>.Of("test");

                    // Act
                    var result = optional.ThrowIfNotEqual("test", new Exception());

                    // Assert
                    result.Should().BeSameAs(optional);
                }

                [Fact]
                public void InstanceIsObject()
                {
                    // Arrange
                    var optional = Optional<object>.Of(new { Name = "test", Description = "description" });

                    // Act
                    var result = optional.ThrowIfNotEqual(new { Name = "test", Description = "description" },
                        new Exception());

                    // Assert
                    result.Should().BeSameAs(optional);
                }

                [Fact]
                public void InstanceIsObjectWithNull()
                {
                    // Arrange
                    var optional = Optional<object>.Of(null);

                    // Act
                    var result = optional.ThrowIfNotEqual(null, new Exception());

                    // Assert
                    result.Should().BeSameAs(optional);
                }
            }
        }
    }

    public class Get
    {
        public class ShouldThrowException
        {
            [Fact]
            public void WhenInstanceIsNull()
            {
                // Arrange
                var optional = Optional<object>.Of(null);

                // Act
                Action act = () => optional.Get();

                // Assert
                act.Should().Throw<InvalidOperationException>().WithMessage("Instance is null.");
            }
        }

        public class ShouldReturnValue
        {
            [Fact]
            public void WhenInstanceIsNotNull()
            {
                // Arrange
                var value = new object();
                var optional = Optional<object>.Of(value);

                // Act
                var result = optional.Get();

                // Assert
                result.Should().BeSameAs(value);
            }
        }
    }
}