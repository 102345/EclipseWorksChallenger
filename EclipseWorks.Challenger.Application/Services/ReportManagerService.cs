using EclipseWorks.Challenger.Application.Services.Interfaces;
using EclipseWorks.Challenger.Domain.Entities;
using EclipseWorks.Challenger.InfraStructure.Interfaces;

namespace EclipseWorks.Challenger.Application.Services
{
    public class ReportManagerService : IReportManagerService
    {
        public IUnitOfWork _unitOfWork { get; }
        public ReportManagerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

        }
        public async Task<IEnumerable<ReportManager>> GetAllDeliveries(int? idProject, int? Status, int? idOwner)
        {   
            _unitOfWork.BeginTransaction();

            var reportManagers = await _unitOfWork.ReportManagers.GetAllDeliveries(idProject, Status, idOwner);

            _unitOfWork.Commit();   

            return reportManagers;


        }
    }
}
