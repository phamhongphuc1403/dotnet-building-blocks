using System.Globalization;

namespace BuildingBlock.Core.Domain.Shared.Utils;

public static class DoubleUtility
{
    public static string ToString(double value)
    {
        var valueString = value.ToString(CultureInfo.InvariantCulture);

        return valueString.Replace('.', ',');
    }
}