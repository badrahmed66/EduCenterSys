using EduCenterSys.Domain.Interfaces;

namespace EduCenterSys.Domain.Entities;

public class Student(string name, string phoneNumber, string address, string guardianPhoneNumber, int gradeId) 
{
    public int StudentId { get; private set; }
    public string Name { get; private set; } = name;
    public string PhoneNumber { get; private set; } = phoneNumber;
    public string Address { get; private set; } = address;
    public string GuardianPhoneNumber { get; private set; } = guardianPhoneNumber;
    public int GradeId { get; private set; } = gradeId;
    public Grade Grade { get; private set; } = default!;

    public ICollection<StudentEnrollment> Enrollments { get; private set; } = [];

}