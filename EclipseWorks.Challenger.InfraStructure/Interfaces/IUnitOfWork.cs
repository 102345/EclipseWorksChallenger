using EclipseWorks.Challenger.Domain.Repositories.Interfaces;

namespace EclipseWorks.Challenger.InfraStructure.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IOwnerRepository Owners { get; }
        IProjectRepository Projects { get; }
        ITaskProjectRepository TaskProjects { get; }

        IReportManagerRepository ReportManagers { get; }

        ICommentRepository Comments { get; }

        IHistoryTaskProjectRepository HistoryTaskProjects { get; }

        void BeginTransaction();
        void Commit();
        void Rollback();
    }
}
