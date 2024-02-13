using EclipseWorks.Challenger.Domain.Entities;

namespace EclipseWorks.Challenger.Domain.Repositories.Interfaces
{
    public interface ITaskProjectRepository
    {
        Task<IEnumerable<TaskProject>> GetByProject(int idProject);
        Task<TaskProject> GetById(int id);
        Task<int> Add(TaskProject taskProject);
        Task Update(TaskProject taskProject);
        Task Delete(int id);
    }
}
