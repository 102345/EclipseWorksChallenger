using EclipseWorks.Challenger.Domain.Entities;

namespace EclipseWorks.Challenger.Application.Services.Interfaces
{
    public interface ITaskProjectService
    {
        Task<IEnumerable<TaskProject>> GetAllTasks(int idProject);
        Task CreateTaskAsync(TaskProject taskProject);
        Task DeleteTaskAsync(int id);
        Task UpdateTaskAsync(TaskProject taskProject);
    }
}
