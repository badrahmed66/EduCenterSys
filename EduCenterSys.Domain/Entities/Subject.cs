namespace EduCenterSys.Domain.Entities;

public class Subject
{
    private Subject() { }
    public Subject(string name)
    {
        Name = name;
    }
    public int SubjectId { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public ICollection<Group> Groups { get; private set; } = [];
    public ICollection<TeacherQualification> TeacherQualifications { get; private set; } = [];
}
