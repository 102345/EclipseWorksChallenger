using AutoMapper;
using EclipseWorks.Challenger.Application.Contracts;
using EclipseWorks.Challenger.Domain.Entities;

namespace EclipeWorks.Challenger.Api.Profiles
{
    public class ProjectProfile : Profile
    {
        public ProjectProfile()
        {
            CreateMap<Project, ProjectModel>();
            CreateMap<ProjectModel, Project>();
        }
    }
}
