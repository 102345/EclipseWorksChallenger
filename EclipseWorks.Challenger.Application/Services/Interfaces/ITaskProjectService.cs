using EclipseWorks.Challenger.Domain.Entities;

namespace EclipseWorks.Challenger.Application.Services.Interfaces
{
    public interface ITaskProjectService
    {
        Task<TaskProject> GetById(int idTask);
        Task<IEnumerable<TaskProject>> GetAllTasks(int idProject);
        Task<bool> CreateTaskAsync(TaskProject taskProject);
        Task<bool> DeleteTaskAsync(int id);
        Task<bool> UpdateTaskAsync(TaskProject taskProject);
    }
}
