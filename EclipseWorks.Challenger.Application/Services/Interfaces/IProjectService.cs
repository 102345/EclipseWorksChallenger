using EclipseWorks.Challenger.Domain.Entities;

namespace EclipseWorks.Challenger.Application.Services.Interfaces
{
    public interface IProjectService
    {
        Task<IEnumerable<Project>> GetAllAsync(int idOwner);
        Task CreateAsync(Project project);
    }
}
