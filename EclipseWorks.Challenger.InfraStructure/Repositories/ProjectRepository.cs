using Dapper;
using EclipseWorks.Challenger.Domain.Entities;
using EclipseWorks.Challenger.Domain.Repositories.Interfaces;
using System.Data;
using System.Diagnostics.CodeAnalysis;

namespace EclipseWorks.Challenger.InfraStructure.Repositories
{
    [ExcludeFromCodeCoverage]
    public class ProjectRepository : IProjectRepository
    {
        private readonly IDbConnection _connection;
        private readonly IDbTransaction _transaction;
        public ProjectRepository(IDbConnection connection, IDbTransaction transaction)
        {
            _connection = connection ?? throw new ArgumentNullException(nameof(_connection));
            _transaction = transaction ?? throw new ArgumentNullException(nameof(_transaction));
        }
        public async Task Add(Project project)
        {

            var command = @"INSERT INTO [dbo].[Project] (NameProject, Observation,IdOwner,CreatedAt)
                                                    VALUES (@NameProject, @Observation,@IdOwner,getdate())";

            await _connection.ExecuteAsync(command, new
            {
                project.NameProject,
                project.Observation,
                project.IdOwner
            }, _transaction);


        }
        public async Task Delete(int id)
        {
            var command = @"DELETE FROM [dbo].[Project] WHERE IdProject = @id";

            await _connection.ExecuteAsync(command, new
            {
                id
            }, _transaction);

        }

        public async Task<IEnumerable<Project>> GetAllAsync(int idOwner)
        {
            var sql = @"SELECT IdProject , NameProject, Observation, 
                               IdOwner , FORMAT(CreatedAt, 'dd/MM/yyyy') as CreatedAt FROM [dbo].[Project] 
                                    WHERE IdOwner = @idOwner ORDER BY NameProject";

            var projects = await _connection.QueryAsync<Project>(sql, new { idOwner }, _transaction);

            var teste = projects.ToList();

            return projects;

        }

        public async Task<Project> GetById(int id)
        {
            var sql = @"SELECT IdProject,
                              NameProject,  
                              IdOwner,
                              Observation,
                              CreatedAt
                              FROM [dbo].[Project]
                              WHERE IdProject = @IdProject";


            return await _connection.QueryFirstOrDefaultAsync<Project>(sql, new { IdProject = id }, _transaction);
        }

        public async Task Update(Project project)
        {
            throw new NotImplementedException();
        }
    }
}
