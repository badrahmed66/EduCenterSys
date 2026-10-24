
namespace EduCenterSys.Domain.Interfaces;

public interface ISoftDelete
{
    bool IsDeleted { get; }
    DateTimeOffset? DeletedAt { get; }
    void MarkAsDeleted();
}