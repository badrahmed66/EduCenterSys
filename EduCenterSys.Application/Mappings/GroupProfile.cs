
using EduCenterSys.Domain.Entities;

using AutoMapper;
using EduCenterSys.Application.DTOs;

namespace EduCenterSys.Application.Mappings;

public class GroupProfile : Profile
{
    public GroupProfile()
    {

        CreateMap<Group, GroupDtos.Read>()
            .ForCtorParam("EnrollmentsCount",
            opt => opt.MapFrom(src => (byte)(src.Enrollments != null ? src.Enrollments.Count : 0)));

        CreateMap<GroupDtos.Create, Group>();

        CreateMap<GroupDtos.Update, Group>()
            .ForMember(d => d.GradeId, o => o.Condition(s => s.GradeId.HasValue))
            .ForMember(d => d.SubjectId, o => o.Condition(s => s.SubjectId.HasValue))
            .ForMember(d => d.TeacherId, o => o.Condition(s => s.TeacherId.HasValue))
            .ForMember(d => d.Capacity, o => o.Condition(s => s.Capacity.HasValue))
            .ForMember(d => d.Status, o => o.Condition(s => s.Status.HasValue))
            .ForMember(d => d.Schedules, o => o.Condition(s => s.Schedules != null));

    }
}