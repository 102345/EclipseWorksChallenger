using Dapper;
using EclipseWorks.Challenger.Domain.Entities;
using EclipseWorks.Challenger.Domain.Repositories.Interfaces;
using System.Data;

namespace EclipseWorks.Challenger.InfraStructure.Repositories
{
    public class OwnerRepository : IOwnerRepository
    {
        private readonly IDbConnection _connection;
        private readonly IDbTransaction _transaction;
        public OwnerRepository(IDbConnection connection, IDbTransaction transaction)
        {
            _connection = connection ?? throw new ArgumentNullException(nameof(_connection));
            _transaction = transaction ?? throw new ArgumentNullException(nameof(_transaction));
        }

        public async Task<Owner> GetById(int id)
        {
            return await _connection.QuerySingleOrDefaultAsync<Owner>("SELECT IdOwner ,Name, IdPosition FROM Owner WHERE IdOwner = @IdOwner", new { IdOwner = id }, _transaction);
        }
    }
}
