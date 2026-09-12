
namespace EduCenterSys.Domain.Interfaces;

public interface ISoftDelete
{
    bool IsDeleted { get; set; }
    DateTimeOffset DeletedAt { get; set; }
}