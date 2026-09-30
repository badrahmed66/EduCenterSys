
using AutoMapper;
using EduCenterSys.Application.DTOs;
using EduCenterSys.Domain.Entities;

namespace EduCenterSys.Application.Mappings;

public class GradeProfile : Profile
{
    public GradeProfile()
    {
        CreateMap<Grade, GradeDtos.Read>();

        CreateMap<GradeDtos.Create, Grade>();

        CreateMap<GradeDtos.Update, Grade>();
    }
}