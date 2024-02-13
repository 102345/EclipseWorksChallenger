using Dapper;
using EclipseWorks.Challenger.Domain.Entities;
using EclipseWorks.Challenger.Domain.Repositories.Interfaces;
using System.Data;
using System.Text;

namespace EclipseWorks.Challenger.InfraStructure.Repositories
{
    public class ReportManagerRepository :IReportManagerRepository
    {
        private readonly IDbConnection _connection;
        private readonly IDbTransaction _transaction;
        public ReportManagerRepository(IDbConnection connection, IDbTransaction transaction)
        {
            _connection = connection ?? throw new ArgumentNullException(nameof(_connection));
            _transaction = transaction ?? throw new ArgumentNullException(nameof(_transaction));
        }

        public async Task<IEnumerable<ReportManager>> GetAllDeliveries(int? idProject, int? Status, int? idOwner)
        {
			StringBuilder sqlQuery = new StringBuilder();

			sqlQuery.Append(@" SELECT p.NameProject,
							 COUNT(t.IdTask) CountTask,
							 CASE 
								WHEN t.Status = 0 THEN 'Pendency'
								WHEN t.Status = 1 THEN 'In Progress'
								ELSE 'Completed'
							 END AS Task,
							 o.Name,
							 CASE 
								WHEN o.IdPosition = 1 THEN 'Manager'
								WHEN o.IdPosition = 2 THEN 'Administrator'
								ELSE 'SuportIT'
							 END AS Position
							 FROM Task t
							 INNER JOIN Project p ON p.IdProject = t.IdProject
							 INNER JOIN Owner  o  ON o.IdOwner = t.IdOwner
							 WHERE  
									FORMAT(t.DueDate, 'yyyy-MM-dd') BETWEEN FORMAT(getdate() -30, 'yyyy-MM-dd') AND FORMAT(getdate(), 'yyyy-MM-dd') ");

			if (idProject != null) sqlQuery.Append(string.Format(" AND p.IdProject = {0}", idProject));
            if (Status != null) sqlQuery.Append(string.Format(" AND t.Status = {0}", Status));
            if (idOwner != null) sqlQuery.Append(string.Format(" AND t.IdOwner = {0}", idOwner));

            sqlQuery.Append(@"GROUP BY p.NameProject,
									  (CASE 
										WHEN t.Status = 0 THEN 'Pendency'
										WHEN t.Status = 1 THEN 'In Progress'
										ELSE 'Completed'
										END),
									  o.Name,
									  (CASE 
											WHEN o.IdPosition = 1 THEN 'Manager'
											WHEN o.IdPosition = 2 THEN 'Administrator'
											ELSE 'SuportIT'
										 END) ORDER BY o.Name, Task");

            return await _connection.QueryAsync<ReportManager>(sqlQuery.ToString(),null, _transaction);

        }
    }
}
