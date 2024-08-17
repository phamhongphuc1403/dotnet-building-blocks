using System.Text;
using BuildingBlock.Core.Domain.ValueObjects.Abstractions;

namespace BuildingBlock.Core.Domain.Shared.Utils;

public static class ValueObjectUtility
{
    public static string ToNormalizedName(this ValueObject valueObject)
    {
        var name = valueObject.GetType().Name;
        var stringBuilder = new StringBuilder(name.Length * 2); // Optimizes for potential growth

        stringBuilder.Append(char.ToLower(name[0])); // Start with the first character in lowercase

        for (var i = 1; i < name.Length; i++)
            if (char.IsUpper(name[i]))
            {
                stringBuilder.Append(' ');
                stringBuilder.Append(char.ToLower(name[i]));
            }
            else
            {
                stringBuilder.Append(name[i]);
            }

        return stringBuilder.ToString();
    }
}