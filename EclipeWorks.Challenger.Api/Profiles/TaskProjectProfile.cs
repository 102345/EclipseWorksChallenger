using AutoMapper;
using EclipseWorks.Challenger.Application.Contracts;
using EclipseWorks.Challenger.Domain.Entities;

namespace EclipeWorks.Challenger.Api.Profiles
{
    public class TaskProjectProfile : Profile
    {
        public TaskProjectProfile()
        {
            CreateMap<TaskProject, TaskProjectModel>();
            CreateMap<TaskProjectModel, TaskProject>();
        }
    }
}
