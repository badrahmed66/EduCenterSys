
using EduCenterSys.Domain.Entities;

using AutoMapper;
using EduCenterSys.Application.DTOs;

namespace EduCenterSys.Application.Mappings;

public class GroupProfile : Profile
{
    public GroupProfile()
    {
        CreateMap<Group, GroupDtos.Read>();
        CreateMap<GroupDtos.Create, Group>();
        CreateMap<GroupDtos.Update, Group>();
    }
}