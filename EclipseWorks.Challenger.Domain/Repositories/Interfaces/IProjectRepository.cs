using EclipseWorks.Challenger.Domain.Entities;

namespace EclipseWorks.Challenger.Domain.Repositories.Interfaces
{
    public interface IProjectRepository
    {   
        Task<IEnumerable<Project>> GetAllAsync(int idOwner);
        Task<Project> GetById(int id);
        Task Add(Project project);
        Task Update(Project project);
        Task Delete(int id);
    }
}
