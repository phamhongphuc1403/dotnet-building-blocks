using BuildingBlock.Core.Domain.Exceptions;
using BuildingBlock.Core.Domain.Repositories;
using BuildingBlock.Core.Domain.Shared.Utils;
using BuildingBlock.Core.Domain.Specifications.Abstractions;
using FluentAssertions;
using Moq;

namespace Tests.Core.Domain.Shared.Utils;

public class EntityHelperTest
{
    public class TestEntityNotFoundException : EntityNotFoundException
    {
        public TestEntityNotFoundException(string message) : base(message)
        {
        }
    }

    public class TestEntityConflictException : EntityConflictException
    {
        public TestEntityConflictException(string message) : base(message)
        {
        }
    }

    public class GetOrThrowAsync
    {
        public class ShouldThrowException
        {
            public class WhenEntityIsNull
            {
                private readonly Mock<IReadOnlyRepository<TestEntity>> _mockEntityRepositoryMock;

                public WhenEntityIsNull()
                {
                    _mockEntityRepositoryMock = new Mock<IReadOnlyRepository<TestEntity>>();
                }

                [Fact]
                public void CaseSpecification()
                {
                    // Arrange
                    _mockEntityRepositoryMock.Setup(repo =>
                            repo.GetAnyAsync(It.IsAny<Specification<TestEntity>>(), null, false, false))
                        .ReturnsAsync((TestEntity?)null);
                    const string exceptionMessage = "Entity not found.";

                    // Act
                    Func<Task> act = async () => await EntityHelper.GetOrThrowAsync(null,
                        new TestEntityNotFoundException(exceptionMessage), _mockEntityRepositoryMock.Object);

                    // Assert
                    act.Should().ThrowAsync<EntityNotFoundException>().WithMessage(exceptionMessage);
                }

                [Fact]
                public void CaseId()
                {
                    // Arrange
                    _mockEntityRepositoryMock.Setup(repo =>
                            repo.GetAnyAsync(It.IsAny<Specification<TestEntity>>(), null, false, false))
                        .ReturnsAsync((TestEntity?)null);
                    const string exceptionMessage = "Entity not found.";

                    // Act
                    Func<Task> act = async () => await EntityHelper.GetOrThrowAsync(Guid.NewGuid(),
                        new TestEntityNotFoundException(exceptionMessage), _mockEntityRepositoryMock.Object);

                    // Assert
                    act.Should().ThrowAsync<EntityNotFoundException>().WithMessage(exceptionMessage);
                }
            }

            public class ShouldReturnValue
            {
                public class WhenEntityIsNotNull
                {
                    private readonly Mock<IReadOnlyRepository<TestEntity>> _mockEntityRepositoryMock;

                    public WhenEntityIsNotNull()
                    {
                        _mockEntityRepositoryMock = new Mock<IReadOnlyRepository<TestEntity>>();
                    }

                    [Fact]
                    public async Task CaseSpecification()
                    {
                        // Arrange
                        var entity = new TestEntity();
                        _mockEntityRepositoryMock.Setup(repo =>
                                repo.GetAnyAsync(It.IsAny<Specification<TestEntity>>(), null, false, false))
                            .ReturnsAsync(entity);

                        // Act
                        var result = await EntityHelper.GetOrThrowAsync(null,
                            new TestEntityNotFoundException("Entity not found."), _mockEntityRepositoryMock.Object);

                        // Assert
                        result.Should().Be(entity);
                    }

                    [Fact]
                    public async Task CaseId()
                    {
                        // Arrange
                        var entity = new TestEntity();
                        _mockEntityRepositoryMock.Setup(repo =>
                                repo.GetAnyAsync(It.IsAny<Specification<TestEntity>>(), null, false, false))
                            .ReturnsAsync(entity);

                        // Act
                        var result = await EntityHelper.GetOrThrowAsync(Guid.NewGuid(),
                            new TestEntityNotFoundException("Entity not found."), _mockEntityRepositoryMock.Object);

                        // Assert
                        result.Should().Be(entity);
                    }
                }
            }
        }
    }

    public class ThrowIfNotExistAsync
    {
        public class ShouldThrowException
        {
            public class WhenEntityIsNotExist
            {
                private readonly Mock<IReadOnlyRepository<TestEntity>> _mockEntityRepositoryMock;

                public WhenEntityIsNotExist()
                {
                    _mockEntityRepositoryMock = new Mock<IReadOnlyRepository<TestEntity>>();
                }

                [Fact]
                public void CaseSpecification()
                {
                    // Arrange
                    _mockEntityRepositoryMock.Setup(repo =>
                            repo.CheckIfExistAsync(It.IsAny<Specification<TestEntity>>(), false))
                        .ReturnsAsync(false);
                    const string exceptionMessage = "Entity not found.";

                    // Act
                    var act = async () => await EntityHelper.ThrowIfNotExistAsync(null,
                        new TestEntityNotFoundException(exceptionMessage), _mockEntityRepositoryMock.Object);

                    // Assert
                    act.Should().ThrowAsync<EntityNotFoundException>().WithMessage(exceptionMessage);
                }

                [Fact]
                public void CaseId()
                {
                    // Arrange
                    _mockEntityRepositoryMock.Setup(repo =>
                            repo.CheckIfExistAsync(It.IsAny<Specification<TestEntity>>(), false))
                        .ReturnsAsync(false);
                    const string exceptionMessage = "Entity not found.";

                    // Act
                    var act = async () => await EntityHelper.ThrowIfNotExistAsync(Guid.NewGuid(),
                        new TestEntityNotFoundException(exceptionMessage), _mockEntityRepositoryMock.Object);

                    // Assert
                    act.Should().ThrowAsync<EntityNotFoundException>().WithMessage(exceptionMessage);
                }
            }
        }

        public class ShouldNotThrowException
        {
            public class WhenEntityIsExist
            {
                private readonly Mock<IReadOnlyRepository<TestEntity>> _mockEntityRepositoryMock;

                public WhenEntityIsExist()
                {
                    _mockEntityRepositoryMock = new Mock<IReadOnlyRepository<TestEntity>>();
                }

                [Fact]
                public async Task CaseSpecification()
                {
                    // Arrange
                    _mockEntityRepositoryMock.Setup(repo =>
                            repo.CheckIfExistAsync(It.IsAny<Specification<TestEntity>>(), false))
                        .ReturnsAsync(true);

                    // Act
                    var act = async () => await EntityHelper.ThrowIfNotExistAsync(null,
                        new TestEntityNotFoundException("Entity not found."), _mockEntityRepositoryMock.Object);

                    // Assert
                    await act.Should().NotThrowAsync<EntityNotFoundException>();
                }

                [Fact]
                public async Task CaseId()
                {
                    // Arrange
                    _mockEntityRepositoryMock.Setup(repo =>
                            repo.CheckIfExistAsync(It.IsAny<Specification<TestEntity>>(), false))
                        .ReturnsAsync(true);

                    // Act
                    var act = async () => await EntityHelper.ThrowIfNotExistAsync(Guid.NewGuid(),
                        new TestEntityNotFoundException("Entity not found."), _mockEntityRepositoryMock.Object);

                    // Assert
                    await act.Should().NotThrowAsync<EntityNotFoundException>();
                }
            }
        }
    }

    public class ThrowIfExistAsync
    {
        public class ShouldThrowException
        {
            public class WhenEntityIsExist
            {
                private readonly Mock<IReadOnlyRepository<TestEntity>> _mockEntityRepositoryMock;

                public WhenEntityIsExist()
                {
                    _mockEntityRepositoryMock = new Mock<IReadOnlyRepository<TestEntity>>();
                }

                [Fact]
                public void CaseSpecification()
                {
                    // Arrange
                    _mockEntityRepositoryMock.Setup(repo =>
                            repo.CheckIfExistAsync(It.IsAny<Specification<TestEntity>>(), false))
                        .ReturnsAsync(true);
                    const string exceptionMessage = "Entity already exist.";

                    // Act
                    var act = async () => await EntityHelper.ThrowIfExistAsync(null,
                        new TestEntityConflictException(exceptionMessage), _mockEntityRepositoryMock.Object);

                    // Assert
                    act.Should().ThrowAsync<TestEntityConflictException>().WithMessage(exceptionMessage);
                }

                [Fact]
                public void CaseId()
                {
                    // Arrange
                    _mockEntityRepositoryMock.Setup(repo =>
                            repo.CheckIfExistAsync(It.IsAny<Specification<TestEntity>>(), false))
                        .ReturnsAsync(true);
                    const string exceptionMessage = "Entity already exist.";

                    // Act
                    var act = async () => await EntityHelper.ThrowIfExistAsync(Guid.NewGuid(),
                        new TestEntityConflictException(exceptionMessage), _mockEntityRepositoryMock.Object);

                    // Assert
                    act.Should().ThrowAsync<TestEntityConflictException>().WithMessage(exceptionMessage);
                }
            }
        }

        public class ShouldNotThrowExeption
        {
            public class WhenEntityIsNotExist
            {
                private readonly Mock<IReadOnlyRepository<TestEntity>> _mockEntityRepositoryMock;

                public WhenEntityIsNotExist()
                {
                    _mockEntityRepositoryMock = new Mock<IReadOnlyRepository<TestEntity>>();
                }

                [Fact]
                public async Task CaseSpecification()
                {
                    // Arrange
                    _mockEntityRepositoryMock.Setup(repo =>
                            repo.CheckIfExistAsync(It.IsAny<Specification<TestEntity>>(), false))
                        .ReturnsAsync(false);

                    // Act
                    var act = async () => await EntityHelper.ThrowIfExistAsync(null,
                        new TestEntityConflictException("Entity already exist."), _mockEntityRepositoryMock.Object);

                    // Assert
                    await act.Should().NotThrowAsync<TestEntityConflictException>();
                }

                [Fact]
                public async Task CaseId()
                {
                    // Arrange
                    _mockEntityRepositoryMock.Setup(repo =>
                            repo.CheckIfExistAsync(It.IsAny<Specification<TestEntity>>(), false))
                        .ReturnsAsync(false);

                    // Act
                    var act = async () => await EntityHelper.ThrowIfExistAsync(Guid.NewGuid(),
                        new TestEntityConflictException("Entity already exist."), _mockEntityRepositoryMock.Object);

                    // Assert
                    await act.Should().NotThrowAsync<TestEntityConflictException>();
                }
            }
        }
    }
}