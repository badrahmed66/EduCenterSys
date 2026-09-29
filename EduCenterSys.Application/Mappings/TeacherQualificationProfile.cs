
using AutoMapper;
using EduCenterSys.Domain.Entities;
using EduCenterSys.Application.DTOs;

namespace EduCenterSys.Application.Mappings;

public class TeacherQualificationProfile : Profile
{
    public TeacherQualificationProfile()
    {
        CreateMap<TeacherQualification, TeacherQualificationDtos.Read>();

        CreateMap<TeacherQualificationDtos.Create, TeacherQualification>();

        CreateMap<TeacherQualificationDtos.Update, TeacherQualification>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    }
}