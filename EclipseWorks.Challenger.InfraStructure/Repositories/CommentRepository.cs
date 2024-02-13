using Dapper;
using EclipseWorks.Challenger.Domain.Entities;
using EclipseWorks.Challenger.Domain.Repositories.Interfaces;
using System.Data;

namespace EclipseWorks.Challenger.InfraStructure.Repositories
{
    public class CommentRepository : ICommentRepository
    {

        private readonly IDbConnection _connection;
        private readonly IDbTransaction _transaction;
        public CommentRepository(IDbConnection connection, IDbTransaction transaction)
        {
            _connection = connection ?? throw new ArgumentNullException(nameof(_connection));
            _transaction = transaction ?? throw new ArgumentNullException(nameof(_transaction));
        }
        public async Task<int> Add(Comment comment)
        {

            var command = @"INSERT INTO [dbo].[Comment] (IdTask,Description,CreatedAt)
                                                    VALUES (@IdTask,@Description,getdate());
                                    SELECT CAST(SCOPE_IDENTITY() as int);";

            var id = await _connection.QueryAsync<int>(command, comment, _transaction);

            return id.Single();


        }

        public async Task Delete(int id)
        {

            var command = @"DELETE FROM [dbo].[Comment] WHERE IdComment = @id";

            await _connection.ExecuteAsync(command, new
            {
                id
            }, _transaction);


        }

        public async Task DeletePerTask(int idTask)
        {
            var command = @"DELETE FROM [dbo].[Comment] WHERE IdTask = @idTask";

            await _connection.ExecuteAsync(command, new
            {
                idTask
            }, _transaction);
        }

        public async Task<IEnumerable<Comment>> GetCommentsByTask(int idTask)
        {

            var sql = @"SELECT IdComment,
                              IdTask,  
                              Description,
                              CreatedAt
                              FROM [dbo].[Comment]
                              WHERE IdTask = @IdTask";


            return await _connection.QueryAsync<Comment>(sql, new { IdTask = idTask }, _transaction);
        }
    }
}
