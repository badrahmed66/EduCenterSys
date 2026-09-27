namespace EduCenterSys.Domain.Entities;

public class TeacherQualification
{
    private TeacherQualification() { }
    public TeacherQualification(int teacherId, int subjectId, int gradeId)
    {
        TeacherId = teacherId;
        SubjectId = subjectId;
        GradeId = gradeId;
    }
    public int TeacherQualificationId { get; private set; }
    public int TeacherId { get; private set; }
    public int SubjectId { get; private set; }
    public int GradeId { get; private set; }
    public Subject Subject { get; private set; } = default!;
    public Grade Grade { get; private set; } = default!;
    public Teacher Teacher { get; private set; } = default!;
}
