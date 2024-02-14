using EclipseWorks.Challenger.Application.Services.Interfaces;
using EclipseWorks.Challenger.Domain.Enums;
using EclipseWorks.Challenger.InfraStructure.Interfaces;

namespace EclipseWorks.Challenger.Application.Services
{
    public class ReportManagerValidatorService : IReportManagerValidatorService
    {

        public IUnitOfWork _unitOfWork { get; }
        public ReportManagerValidatorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

        }
        public async Task<bool> IsManager(int idOwnerAuthorized)
        {


            var owner = await _unitOfWork.Owners.GetById(idOwnerAuthorized);

            return owner.IdPosition == (int)EnumRoleOwner.Manager ? true : false;



        }
    }
}
