namespace EduCenterSys.Domain.Entities;

public class Subject(string name)
{
    public int SubjectId { get; private set; }
    public string Name { get; private set; } = name;
    public ICollection<Group> Groups { get; private set; } = [];
    public ICollection<TeacherQualification> TeacherQualifications { get; private set; } = [];
}
