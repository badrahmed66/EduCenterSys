
using AutoMapper;
using EduCenterSys.Domain.Entities;
using EduCenterSys.Application.DTOs;

namespace EduCenterSys.Application.Mappings;

public class TeacherQualificationProfile : Profile
{
    public TeacherQualificationProfile()
    {
        CreateMap<TeacherQualification, TeacherQualificationDtos.Read>()
            .ForMember(dest => dest.GradeName, opt => opt.MapFrom(src => src.Grade.Name))
            .ForMember(dest => dest.SubjectName, opt => opt.MapFrom(src => src.Subject.Name));

        CreateMap<TeacherQualificationDtos.Create, TeacherQualification>();

        CreateMap<TeacherQualificationDtos.Update, TeacherQualification>();
    }
}