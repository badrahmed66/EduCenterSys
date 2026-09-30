
using AutoMapper;

namespace EduCenterSys.Application.Helpers;

public class CleanStringConverter : ITypeConverter<string?, string?>
{

    public string? Convert(string? source, string? destination, ResolutionContext context)
    {
        if (string.IsNullOrWhiteSpace(source))
            return null;

        return source.ToStandardFormat();
    }
}