using Dapper;
using EclipseWorks.Challenger.Domain.Entities;
using EclipseWorks.Challenger.Domain.Repositories.Interfaces;
using System.Data;
using System.Diagnostics.CodeAnalysis;

namespace EclipseWorks.Challenger.InfraStructure.Repositories
{
    [ExcludeFromCodeCoverage]
    public class HistoryTaskProjectRepository : IHistoryTaskProjectRepository
    {
        private readonly IDbConnection _connection;
        private readonly IDbTransaction _transaction;
        public HistoryTaskProjectRepository(IDbConnection connection, IDbTransaction transaction)
        {
            _connection = connection ?? throw new ArgumentNullException(nameof(_connection));
            _transaction = transaction ?? throw new ArgumentNullException(nameof(_transaction));
        }

        public async Task Add(HistoryTaskProject historyTaskProject)
        {

            var command = @"INSERT INTO [dbo].[HistoryTask] ( 
                                                                Status,
                                                                DescriptionTask,
                                                                IdOwner,
                                                                IdProject,
                                                                IdTask,
                                                                IdComment,
                                                                DescriptionComment,
                                                                CreatedAt,
                                                                UpdatedAt,
                                                                DeletedAt
                                                                )
                                                    VALUES (@Status,
                                                            @DescriptionTask,
                                                            @IdOwner,
                                                            @IdProject,
                                                            @IdTask,
                                                            @IdComment,
                                                            @DescriptionComment,
                                                            @CreatedAt,
                                                            @UpdatedAt,
                                                            @DeletedAt)";

            await _connection.ExecuteAsync(command, new
            {
                historyTaskProject.Status,
                historyTaskProject.DescriptionTask,
                historyTaskProject.IdOwner,
                historyTaskProject.IdProject,
                historyTaskProject.IdTask,
                historyTaskProject.IdComment,
                historyTaskProject.DescriptionComment,
                historyTaskProject.CreatedAt,
                historyTaskProject.UpdatedAt,
                historyTaskProject.DeletedAt
            }, _transaction);

        }
    }
}
