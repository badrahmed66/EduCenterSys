
namespace EduCenterSys.Application.DTOs;

public class SubjectDtos
{
    public record Read(int SubjectId, string Name);
    public record Create(string Name);
    public record Update(string Name);

}