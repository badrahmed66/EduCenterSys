namespace EduCenterSys.Domain.Entities;

public class Grade
{
    private Grade() { }
    public Grade(string name)
    {
        Name = name;
    }
    public int GradeId { get; private set; }
    public string Name { get; private set; } = string.Empty;

    public ICollection<Student> Students { get; private set; } = [];
    public ICollection<Group> Groups { get; private set; } = [];
    public ICollection<TeacherQualification> TeacherQualifications { get; private set; } = [];
}
