using EclipseWorks.Challenger.Domain.Entities;

namespace EclipseWorks.Challenger.Domain.Repositories.Interfaces
{
    public interface IOwnerRepository
    {
        Task<Owner> GetById(int id);
    }
}
