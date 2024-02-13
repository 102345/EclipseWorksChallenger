using EclipseWorks.Challenger.Application.Services.Interfaces;
using EclipseWorks.Challenger.Domain.Enums;
using EclipseWorks.Challenger.InfraStructure.Interfaces;
using EclipseWorks.Challenger.InfraStructure.UnitOfWork;

namespace EclipseWorks.Challenger.Application.Services
{
    public class ReportManagerValidatorService : IReportManagerValidatorService
    {

        public IReaderStringConnectionDb _readerStringConnectionDb { get; }
        public ReportManagerValidatorService(IReaderStringConnectionDb readerStringConnectionDb)
        {
            _readerStringConnectionDb = readerStringConnectionDb ?? throw new ArgumentNullException(nameof(readerStringConnectionDb));
        }
        public async Task<bool> IsManager(int idOwnerAuthorized)
        {
            
            using (var unitOfWork = new UnitOfWork(_readerStringConnectionDb.GetStringConnectionName()))
            {
                var owner = await unitOfWork.Owners.GetById(idOwnerAuthorized);

                return owner.IdPosition == (int)EnumRoleOwner.Manager ? true : false;
            }


        }
    }
}
