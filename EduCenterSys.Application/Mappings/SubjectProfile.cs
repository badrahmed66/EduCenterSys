
using AutoMapper;
using EduCenterSys.Application.DTOs;
using EduCenterSys.Domain.Entities;

namespace EduCenterSys.Application.Mappings;

public class SubjectProfile : Profile
{
    public SubjectProfile()
    {
        CreateMap<Subject, SubjectDtos.Read>();
        CreateMap<SubjectDtos.Create, Subject>();
        CreateMap<SubjectDtos.Update, Subject>();

    }

}