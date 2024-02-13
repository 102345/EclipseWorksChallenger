using AutoMapper;
using EclipseWorks.Challenger.Application.Contracts;
using EclipseWorks.Challenger.Domain.Entities;

namespace EclipeWorks.Challenger.Api.Profiles
{
    public class ProjectModelResponseProfile : Profile
    {
        public ProjectModelResponseProfile() {

            CreateMap<Project, ProjectModelResponse>()
                .ForMember(d => d.CreatedAt, o => o.MapFrom(s =>s.CreatedAt));

            CreateMap<ProjectModelResponse, Project>();

        }
    }
}
