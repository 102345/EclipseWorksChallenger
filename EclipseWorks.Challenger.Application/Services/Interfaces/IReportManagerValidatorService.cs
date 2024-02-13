

namespace EclipseWorks.Challenger.Application.Services.Interfaces
{
    public interface IReportManagerValidatorService
    {
        Task<bool> IsManager(int idOwnerAuthorized);
    }
}
