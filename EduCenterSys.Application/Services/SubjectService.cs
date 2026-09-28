
using AutoMapper;
using EduCenterSys.Application.DTOs;
using EduCenterSys.Application.Interfaces;
using EduCenterSys.Domain.Entities;
using EduCenterSys.Domain.Interfaces;

namespace EduCenterSys.Application.Services;

public class SubjectService : BaseService<Subject, SubjectDtos.Read, SubjectDtos.Create, SubjectDtos.Update>,ISubjectService
{
    public SubjectService(IGenericRepository<Subject> repository, IUnitOfWork unitOfWork, IMapper mapper) : base(repository, unitOfWork, mapper)
    {

    }
}