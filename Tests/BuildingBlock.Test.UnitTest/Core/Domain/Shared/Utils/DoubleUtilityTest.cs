using BuildingBlock.Core.Domain.Shared.Utils;
using FluentAssertions;

namespace Tests.Core.Domain.Shared.Utils;

public class DoubleUtilityTest
{
    public class ToString
    {
        [Fact]
        public void ShouldConvertDoubleWithDot()
        {
            var actualData = DoubleUtility.ToString(1.1);

            actualData.Should().Be("1,1");
        }
    }
}