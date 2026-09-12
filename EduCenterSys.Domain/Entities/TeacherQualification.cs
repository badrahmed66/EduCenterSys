namespace EduCenterSys.Domain.Entities;

public class TeacherQualification(int teacherId, int subjectId, int gradeId)
{
    public int TeacherQualificationId { get; private set; }
    public int TeacherId { get; private set; } = teacherId;
    public int SubjectId { get; private set; } = subjectId;
    public int GradeId { get; private set; } = gradeId;
    public Subject Subject { get; private set; } = default!;
    public Grade Grade { get; private set; } = default!;
    public Teacher Teacher { get; private set; } = default!;
}
