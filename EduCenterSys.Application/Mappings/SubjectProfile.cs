
using AutoMapper;
using EduCenterSys.Application.DTOs;
using EduCenterSys.Application.Helpers;
using EduCenterSys.Domain.Entities;

namespace EduCenterSys.Application.Mappings;

public class SubjectProfile : Profile
{
    public SubjectProfile()
    {
       // CreateMap<string?, string?>().ConvertUsing<CleanStringConverter>();

        CreateMap<Subject, SubjectDtos.Read>();

        CreateMap<SubjectDtos.Create, Subject>();

        CreateMap<SubjectDtos.Update, Subject>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

    }

}