
namespace EduCenterSys.Application.Helpers;

public static class StringExtensions
{
    public static string? ToStandardFormat(this string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return null;

        var clean = System.Text.RegularExpressions.Regex.Replace(input.Trim(), @"\s+", "");

        return clean.ToLowerInvariant();
    }
}