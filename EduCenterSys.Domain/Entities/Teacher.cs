using EduCenterSys.Domain.Enums;

namespace EduCenterSys.Domain.Entities;

public class Teacher(string name, string address, string phoneNumber, EntityStatus status)
{
    public int TeacherId { get; private set; }
    public string Name { get; private set; } = name;
    public string Address { get; private set; } = address;
    public string PhoneNumber { get; private set; } = phoneNumber;
    public EntityStatus Status { get; private set; } = status;
    public ICollection<Group> Groups{get;private set;}=[];
    public ICollection<TeacherQualification> Qualifications{get;private set;}=[];
}
