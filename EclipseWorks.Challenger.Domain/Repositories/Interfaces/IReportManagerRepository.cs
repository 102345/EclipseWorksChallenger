using EclipseWorks.Challenger.Domain.Entities;

namespace EclipseWorks.Challenger.Domain.Repositories.Interfaces
{
    public interface IReportManagerRepository
    {
        Task<IEnumerable<ReportManager>> GetAllDeliveries(int? idProject , int? Status, int? idOwner);
    }
}
