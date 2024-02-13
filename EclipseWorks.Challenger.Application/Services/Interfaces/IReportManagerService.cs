using EclipseWorks.Challenger.Domain.Entities;

namespace EclipseWorks.Challenger.Application.Services.Interfaces
{
    public interface IReportManagerService
    {
        Task<IEnumerable<ReportManager>> GetAllDeliveries(int? idProject, int? Status, int? idOwner);
    }
}
