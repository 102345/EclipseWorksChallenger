using EclipseWorks.Challenger.Domain.Entities;

namespace EclipseWorks.Challenger.Application.Services.Interfaces
{
    public interface IProjectService
    {
        Task<Project> GetById(int id);
        Task<IEnumerable<Project>> GetAllAsync(int idOwner);
        Task CreateAsync(Project project);

        Task DeleteAsync(int idProject);
    }
}
