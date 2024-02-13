using EclipseWorks.Challenger.Domain.Entities;

namespace EclipseWorks.Challenger.Domain.Repositories.Interfaces
{
    public interface ICommentRepository
    {
        Task<int> Add(Comment project);
        Task Delete(int id);
        Task DeletePerTask(int idTask);
    }
}
