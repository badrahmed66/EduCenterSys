using EduCenterSys.Domain.Enums;

namespace EduCenterSys.Domain.Entities;

public class Grade
{
    private Grade() { }
    public Grade(GradeLevel level)
    {
        Level = level;
    }
    public int GradeId { get; private set; }
    public GradeLevel Level { get; private set; }

    public ICollection<Student> Students { get; private set; } = [];
    public ICollection<Group> Groups { get; private set; } = [];
    public ICollection<TeacherQualification> TeacherQualifications { get; private set; } = [];
}
