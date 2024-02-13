using EclipseWorks.Challenger.Domain.Entities;

namespace EclipseWorks.Challenger.Domain.Repositories.Interfaces
{
    public interface IHistoryTaskProjectRepository
    {
        Task Add(HistoryTaskProject historyTaskProject);
    }
}
