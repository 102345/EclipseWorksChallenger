using EclipseWorks.Challenger.Application.Services.Interfaces;
using EclipseWorks.Challenger.Domain.Entities;
using EclipseWorks.Challenger.InfraStructure.Interfaces;
using EclipseWorks.Challenger.InfraStructure.UnitOfWork;

namespace EclipseWorks.Challenger.Application.Services
{
    public class ReportManagerService : IReportManagerService
    {
        public IReaderStringConnectionDb _readerStringConnectionDb { get; }
        public ReportManagerService(IReaderStringConnectionDb readerStringConnectionDb)
        {
            _readerStringConnectionDb = readerStringConnectionDb ?? throw new ArgumentNullException(nameof(readerStringConnectionDb));
        }

        public async Task<IEnumerable<ReportManager>> GetAllDeliveries(int? idProject, int? Status, int? idOwner)
        {
            using (var unitOfWork = new UnitOfWork(_readerStringConnectionDb.GetStringConnectionName()))
            {
                return await unitOfWork.ReportManagers.GetAllDeliveries(idProject, Status, idOwner);

            }
        }
    }
}
