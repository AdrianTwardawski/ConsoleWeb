using System;
using System.Data.SqlClient;

namespace ConsoleWeb.Database
{
    public class DbContext : IDisposable
    {
        private const string ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ChatApp;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public SqlConnection Connection { get; }

        public DbContext()
        {
            Connection = new SqlConnection(ConnectionString);
        }

        public void Dispose()
        {
            Connection.Dispose();
        }
    }
}
