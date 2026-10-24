
using AutoMapper;
using EduCenterSys.Application.DTOs;
using EduCenterSys.Domain.Entities;

namespace EduCenterSys.Application.Mappings;

public class GroupScheduleProfile : Profile
{
    public GroupScheduleProfile()
    {
        CreateMap<GroupSchedule, GroupScheduleDtos.Read>();

        CreateMap<GroupScheduleDtos.Create, GroupSchedule>();

        CreateMap<GroupScheduleDtos.Update, GroupSchedule>();
    }
}