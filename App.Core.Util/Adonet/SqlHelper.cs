using System;
using System.Data;
using System.Data.SqlClient;

namespace App.Core.Util.Adonet
{
    public class SqlHelper : IDisposable
    { 
        public string ConnectionString { get; set; }

        public SqlHelper(string connectionString) => ConnectionString = connectionString;

        public SqlDataReader ExecReaderSp(string procName)
        {
            var sqlConnection = new SqlConnection(connectionString: ConnectionString);
            SqlCommand sqlCommand = null;
            try
            {
                sqlConnection.Open();
                sqlCommand = new SqlCommand(cmdText: procName, connection: sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
            }
            catch (Exception)
            {
                // ignored
            }
            return sqlCommand.ExecuteReader(behavior: CommandBehavior.CloseConnection);
        }
       
        public SqlDataReader ExecReaderSp(string procName, string[] paramNames, object[] paramValues)
        {
            var sqlConnection = new SqlConnection(connectionString: ConnectionString);
            SqlCommand sqlCommand = null;
            try
            {
                sqlConnection.Open();
                sqlCommand = new SqlCommand(cmdText: procName, connection: sqlConnection) { CommandType = CommandType.StoredProcedure };
                for (var index = 0; index < paramNames.Length; ++index)
                {
                    sqlCommand.Parameters.AddWithValue(parameterName: paramNames[index], value: paramValues[index]);
                }
            }
            catch (Exception)
            {
                // ignored
            }
            return sqlCommand.ExecuteReader(behavior: CommandBehavior.CloseConnection);
        }

        public int ExecNonQuerySp(string procName, string[] paramNames, object[] paramValues)
        {
            var sqlConnection = new SqlConnection(connectionString: ConnectionString);
            int numberOfAffectedRows = 0;
            try
            {
                sqlConnection.Open();
                var sqlCommand = new SqlCommand(cmdText: procName, connection: sqlConnection) { CommandType = CommandType.StoredProcedure };
                for (var index = 0; index < paramNames.Length; ++index)
                {
                    sqlCommand.Parameters.AddWithValue(parameterName: paramNames[index], value: paramValues[index]);
                }

                numberOfAffectedRows = sqlCommand.ExecuteNonQuery();
            }
            catch (Exception)
            {
                // ignored
            }
            finally
            {
                sqlConnection.Close();
            }
            return numberOfAffectedRows;
        }

        public void Dispose()
        {
        }
    }
}