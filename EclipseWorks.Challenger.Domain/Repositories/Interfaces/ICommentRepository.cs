using EclipseWorks.Challenger.Domain.Entities;
using System.Linq.Dynamic.Core;

namespace EclipseWorks.Challenger.Domain.Repositories.Interfaces
{
    public interface ICommentRepository
    {  
        Task<IEnumerable<Comment>> GetCommentsByTask(int idTask);
        Task<int> Add(Comment project);
        Task Delete(int id);
        Task DeletePerTask(int idTask);

        Task<PagedResult<Comment>> GetCommentsByTask(int idTask, int pageNumber = 1, int pageSize = 10);
    }
}
