using EduCenterSys.Domain.Interfaces;

namespace EduCenterSys.Domain.Entities;

public class Student
{
    private Student() { }
    public Student(string name, string phoneNumber, string address, string guardianPhoneNumber, int gradeId)
    {
        Name = name;
        PhoneNumber = phoneNumber;
        Address = address;
        GuardianPhoneNumber = guardianPhoneNumber;
        GradeId = gradeId;
    }
    public int StudentId { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string PhoneNumber { get; private set; } = string.Empty;
    public string Address { get; private set; } = string.Empty;
    public string GuardianPhoneNumber { get; private set; } = string.Empty;
    public int GradeId { get; private set; }
    public Grade Grade { get; private set; } = default!;

    public ICollection<StudentEnrollment> Enrollments { get; private set; } = [];

}