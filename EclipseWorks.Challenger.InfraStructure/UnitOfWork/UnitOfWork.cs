using EclipseWorks.Challenger.Domain.Repositories.Interfaces;
using EclipseWorks.Challenger.InfraStructure.Interfaces;
using EclipseWorks.Challenger.InfraStructure.Repositories;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Diagnostics.CodeAnalysis;

namespace EclipseWorks.Challenger.InfraStructure.UnitOfWork
{
    [ExcludeFromCodeCoverage]
    public class UnitOfWork : IUnitOfWork
    {
        private IDbConnection _connection;
        private IDbTransaction _transaction;
        private IOwnerRepository _owners;
        private ITaskProjectRepository _taskProjects;
        private ICommentRepository _comments;
        private IProjectRepository _projects;
        private IReportManagerRepository _reportManagers;
        private IHistoryTaskProjectRepository _historyTaskProjects;
        private bool _disposed;

        public UnitOfWork(string connectionString)
        {
            _connection = new SqlConnection(connectionString);
            _connection.Open();

        }

        public IOwnerRepository Owners
        {
            get { return _owners ?? (_owners = new OwnerRepository(_connection, _transaction)); }
        }

        public ITaskProjectRepository TaskProjects
        {
            get { return _taskProjects ?? (_taskProjects = new TaskProjectRepository(_connection, _transaction)); }
        }


        public ICommentRepository Comments
        {
            get { return _comments ?? (_comments = new CommentRepository(_connection, _transaction)); }
        }

        public IProjectRepository Projects
        {
            get { return _projects ?? (_projects = new ProjectRepository(_connection, _transaction)); }
        }

        public IHistoryTaskProjectRepository HistoryTaskProjects
        {
            get { return _historyTaskProjects ?? (_historyTaskProjects = new HistoryTaskProjectRepository(_connection, _transaction)); }
        }

        public IReportManagerRepository ReportManagers
        {
            get { return _reportManagers ?? (_reportManagers = new ReportManagerRepository(_connection, _transaction)); }
        }
          
        public void Commit()
        {

            _transaction.Commit();

            _transaction.Dispose();
            ResetRepositories();
        }

        public void BeginTransaction()
        {
            _transaction = _connection.BeginTransaction();
        }

        public void Rollback()
        {
            _transaction?.Rollback();
        }


        private void ResetRepositories()
        {
            _owners = null;
            _taskProjects = null;
            _comments = null;
            _projects = null;
            _historyTaskProjects = null;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (_transaction != null)
                    {
                        _transaction.Dispose();
                        _transaction = null;

                    }

                    if (_connection != null)
                    {
                        _connection.Dispose();
                        _connection = null;
                    }

                    ResetRepositories();
                }

                _disposed = true;
            }
        }
    }
}
