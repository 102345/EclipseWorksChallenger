using Dapper;
using EclipseWorks.Challenger.Domain.Entities;
using EclipseWorks.Challenger.Domain.Repositories.Interfaces;
using System.Data;
using System.Diagnostics.CodeAnalysis;

namespace EclipseWorks.Challenger.InfraStructure.Repositories
{
    [ExcludeFromCodeCoverage]
    public class TaskProjectRepository : ITaskProjectRepository
    {

        private readonly IDbConnection _connection;
        private readonly IDbTransaction _transaction;
        public TaskProjectRepository(IDbConnection connection, IDbTransaction transaction)
        {
            _connection = connection ?? throw new ArgumentNullException(nameof(_connection));
            _transaction = transaction ?? throw new ArgumentNullException(nameof(_transaction));
        }
        public async Task<int> Add(TaskProject taskProject)
        {


            var command = @"INSERT INTO [dbo].[Task] (IdPriority, Status,IdProject,Title,Description,DueDate, IdOwner)
                                                    VALUES (@IdPriority, @Status,@IdProject,@Title,@Description,@DueDate,@IdOwner);
                                    SELECT CAST(SCOPE_IDENTITY() as int);";

            var id = await _connection.QueryAsync<int>(command, taskProject, _transaction);

            return id.Single();


        }

        public async Task Delete(int id)
        {

            var command = @"DELETE FROM [dbo].[Task] WHERE IdTask = @id";

            await _connection.ExecuteAsync(command, new
            {
                id
            }, _transaction);


        }

        public async Task<TaskProject> GetById(int id)
        {
            var sql = @"SELECT IdTask,
                              IdPriority,  
                              Status,
                              IdProject,
                              Title,
                              Description,
                              DueDate,
                              IdOwner
                              FROM [dbo].[Task]
                              WHERE IdTask = @IdTask";


            var taskProjects = await _connection.QueryFirstOrDefaultAsync<TaskProject>(sql, new { IdTask = id }, _transaction);

            return taskProjects;



        }

        public async Task<IEnumerable<TaskProject>> GetByProject(int idProject)
        {

            var sql = @"SELECT IdTask,
                              IdPriority,  
                              Status,
                              IdProject,
                              Title,
                              Description,
                              DueDate,
                              IdOwner
                              FROM [dbo].[Task]
                              WHERE IdProject = @IdProject ORDER BY IdTask DESC";


            return await _connection.QueryAsync<TaskProject>(sql, new { IdProject = idProject }, _transaction);
        }

        public async Task Update(TaskProject taskProject)
        {

            var command = @"UPDATE [dbo].[Task] SET Status = @Status,Description = @Description, 
                                                    Title = @Title,
                                                    DueDate = @DueDate,
                                                    IdOwner = @IdOwner
                                                    WHERE IdTask = @IdTask";

            await _connection.ExecuteAsync(command, new
            {
                taskProject.Status,
                taskProject.Description,
                taskProject.Title,
                taskProject.DueDate,
                taskProject.IdOwner,
                taskProject.IdTask
            }, _transaction);

        }
    }
}
