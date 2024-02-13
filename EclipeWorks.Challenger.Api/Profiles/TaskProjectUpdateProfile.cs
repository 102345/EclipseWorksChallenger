using AutoMapper;
using EclipseWorks.Challenger.Application.Contracts;
using EclipseWorks.Challenger.Domain.Entities;

namespace EclipeWorks.Challenger.Api.Profiles
{
    public class TaskProjectUpdateProfile : Profile
    {
        public TaskProjectUpdateProfile()
        {
            CreateMap<TaskProject, TaskProjectUpdateModel>();
            CreateMap<TaskProjectUpdateModel, TaskProject>();
        }
    }
}
