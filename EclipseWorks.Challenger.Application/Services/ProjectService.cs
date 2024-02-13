using EclipseWorks.Challenger.Application.Services.Interfaces;
using EclipseWorks.Challenger.Domain.Entities;
using EclipseWorks.Challenger.InfraStructure.Interfaces;
using EclipseWorks.Challenger.InfraStructure.UnitOfWork;

namespace EclipseWorks.Challenger.Application.Services
{
    public class ProjectService : IProjectService
    {
        public IReaderStringConnectionDb _readerStringConnectionDb { get; }
        public ProjectService(IReaderStringConnectionDb readerStringConnectionDb)
        {
            _readerStringConnectionDb = readerStringConnectionDb ?? throw new ArgumentNullException(nameof(readerStringConnectionDb));
        }
        public async Task CreateAsync(Project project)
        {
            using (var unitOfWork = new UnitOfWork(_readerStringConnectionDb.GetStringConnectionName()))
            {
                await unitOfWork.Projects.Add(project);

                unitOfWork.Commit();
            }
        }

        public async Task<IEnumerable<Project>> GetAllAsync(int idOwner)
        {
            using (var unitOfWork = new UnitOfWork(_readerStringConnectionDb.GetStringConnectionName()))
            {
                return await unitOfWork.Projects.GetAllAsync(idOwner);

            }
        }
    }
}
