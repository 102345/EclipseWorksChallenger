using EclipseWorks.Challenger.Domain.Entities;

namespace EclipseWorks.Challenger.Application.Services.Interfaces
{
    public interface IOwnerService
    {
        Task<Owner> GetById(int id);
    }
}
