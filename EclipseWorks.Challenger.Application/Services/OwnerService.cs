using EclipseWorks.Challenger.Application.Services.Interfaces;
using EclipseWorks.Challenger.Domain.Entities;
using EclipseWorks.Challenger.InfraStructure.Interfaces;

namespace EclipseWorks.Challenger.Application.Services
{
    public class OwnerService : IOwnerService
    {
        public IUnitOfWork _unitOfWork { get; }
        public OwnerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

        }
        public async Task<Owner> GetById(int id)
        {
            _unitOfWork.BeginTransaction();

            var owner = await _unitOfWork.Owners.GetById(id); 

            _unitOfWork.Commit();

            return owner;
        }
    }
}
