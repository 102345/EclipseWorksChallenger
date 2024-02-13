using EclipseWorks.Challenger.Domain.Entities;

namespace EclipseWorks.Challenger.Application.Services.Interfaces
{
    public interface ICommentService
    {
        Task CreateCommentAsync(Comment comment);
    }
}
