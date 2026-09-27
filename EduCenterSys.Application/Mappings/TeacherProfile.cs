
using AutoMapper;
using EduCenterSys.Application.DTOs;
using EduCenterSys.Domain.Entities;

namespace EduCenterSys.Application.Mappings;

public class TeacherProfile : Profile
{
    public TeacherProfile()
    {
        CreateMap<Teacher, TeacherDtos.Read>()
              .ForMember(dest => dest.GroupsNumbers, opt => opt.MapFrom(src => (byte)(src.Groups != null ? src.Groups.Count : 0)));
              
        CreateMap<TeacherDtos.Create, Teacher>();
        CreateMap<TeacherDtos.Update, Teacher>();
    }
}