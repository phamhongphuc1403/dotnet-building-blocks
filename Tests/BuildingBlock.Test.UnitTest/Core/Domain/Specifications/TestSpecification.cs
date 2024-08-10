using System.Linq.Expressions;
using BuildingBlock.Core.Domain.Specifications.Abstractions;

namespace Tests.Core.Domain.Specifications;

public class TestSpecification : Specification<TestEntity>
{
    private readonly bool _result;

    public TestSpecification(bool result)
    {
        _result = result;
    }

    public override Expression<Func<TestEntity, bool>> ToExpression()
    {
        return entity => _result;
    }
}