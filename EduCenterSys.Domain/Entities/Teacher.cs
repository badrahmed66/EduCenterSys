using EduCenterSys.Domain.Enums;

namespace EduCenterSys.Domain.Entities;

public class Teacher
{
    private Teacher() { }
    public Teacher(string name, string address, string phoneNumber, EntityStatus status, string nationalId)
    {
        Name = name;
        Address = address;
        PhoneNumber = phoneNumber;
        Status = status;
        NationalId = nationalId;
    }
    public int TeacherId { get; private set; }
    public string NationalId { get; private set; } = string.Empty;
    public string Name { get; private set; } = string.Empty;
    public string Address { get; private set; } = string.Empty;
    public string PhoneNumber { get; private set; } = string.Empty;
    public EntityStatus Status { get; private set; }
    public ICollection<Group> Groups { get; private set; } = [];
    public ICollection<TeacherQualification> Qualifications { get; private set; } = [];
}
