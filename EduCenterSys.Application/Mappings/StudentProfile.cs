using AutoMapper;
using EduCenterSys.Application.DTOs;
using EduCenterSys.Domain.Entities;
namespace EduCenterSys.Application.Mappings;

public class StudentProfile : Profile
{
    public StudentProfile()
    {
        // get dto for create behavior from the entity [Reading behaviors]
        CreateMap<Student, StudentDtos.Read>();

        // convert dto from creation behavior to an entity [Writing behaviors]
        CreateMap<StudentDtos.Create, Student>();

        // convert dto from updating behavior to an entity [Writing behaviors]
        CreateMap<StudentDtos.Update, Student>();
    }
}