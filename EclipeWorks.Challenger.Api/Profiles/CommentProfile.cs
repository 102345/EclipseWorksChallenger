using AutoMapper;
using EclipseWorks.Challenger.Application.Contracts;
using EclipseWorks.Challenger.Domain.Entities;

namespace EclipeWorks.Challenger.Api.Profiles
{
    public class CommentProfile: Profile
    {
        public CommentProfile()
        {
            CreateMap<Comment, CommentModel>();
            CreateMap<CommentModel, Comment>();
        }
    }
}
