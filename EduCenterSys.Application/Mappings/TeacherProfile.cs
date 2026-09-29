
using AutoMapper;
using AutoMapper.Execution;
using EduCenterSys.Application.DTOs;
using EduCenterSys.Application.Helpers;
using EduCenterSys.Domain.Entities;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EduCenterSys.Application.Mappings;

public class TeacherProfile : Profile
{
    public TeacherProfile()
    {
        //CreateMap<string?, string?>().ConvertUsing<CleanStringConverter>();

        CreateMap<Teacher, TeacherDtos.Read>()
              .ForMember(dest => dest.GroupsNumbers, opt => opt.MapFrom(src => (byte)(src.Groups != null ? src.Groups.Count : 0)));


        CreateMap<TeacherDtos.Create, Teacher>();

        CreateMap<TeacherDtos.Update, Teacher>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

    }
}