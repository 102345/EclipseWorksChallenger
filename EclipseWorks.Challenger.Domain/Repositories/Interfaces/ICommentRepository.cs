using EclipseWorks.Challenger.Domain.Entities;

namespace EclipseWorks.Challenger.Domain.Repositories.Interfaces
{
    public interface ICommentRepository
    {  
        Task<IEnumerable<Comment>> GetCommentsByTask(int idTask);
        Task<int> Add(Comment project);
        Task Delete(int id);
        Task DeletePerTask(int idTask);
    }
}
