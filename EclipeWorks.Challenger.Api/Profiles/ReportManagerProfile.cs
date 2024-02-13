using AutoMapper;
using EclipseWorks.Challenger.Application.Contracts;
using EclipseWorks.Challenger.Domain.Entities;

namespace EclipeWorks.Challenger.Api.Profiles
{
    public class ReportManagerProfile : Profile
    {
        public ReportManagerProfile()
        {
            CreateMap<ReportManager, ReportManagerModel>();
            CreateMap<ReportManagerModel, ReportManager>();
        }
    }
}
