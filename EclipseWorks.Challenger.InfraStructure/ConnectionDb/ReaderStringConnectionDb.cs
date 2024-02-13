using EclipseWorks.Challenger.InfraStructure.Interfaces;

namespace EclipseWorks.Challenger.InfraStructure.ConnectionDb
{
    public class ReaderStringConnectionDb : IReaderStringConnectionDb
    {
        private string _connectionString { get; }
        public ReaderStringConnectionDb(string connectionString) 
        {
            _connectionString = connectionString;
        }
        public string GetStringConnectionName()
        {
           return _connectionString;
        }
    }
}
