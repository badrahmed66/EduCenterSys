
using AutoMapper;
using EduCenterSys.Application.DTOs;
using EduCenterSys.Domain.Entities;
using EduCenterSys.Domain.Interfaces;

namespace EduCenterSys.Application.Mappings;

public class GroupScheduleProfile : Profile
{
    public GroupScheduleProfile()
    {
        CreateMap<IScheduleSlot, GroupSchedule>()
            .ConstructUsing((src, context) =>
                new GroupSchedule(
                    src.Day,
                    src.StartHour,
                    src.EndHour,
                    (int)context.Items["GroupId"]
                ));

        CreateMap<GroupSchedule, GroupScheduleDtos.Read>();
    }
}