using System.Configuration;
using MySql.Data.MySqlClient;

namespace PROG2111_FinalPhase5
{
    /*
     * FILE : DbConnectionFactory.cs
     * PROJECT : PROG2111_FinalPhase5
     * PROGRAMMER : Tuan Thanh Nguyen
     * FIRST VERSION : 12/08/2025
     * DESCRIPTION :
     *   Central factory for creating MySQL connections using the
     *   connection string in App.config.
     */
    internal static class DbConnectionFactory
    {
        private static readonly string ConnectionString =
            ConfigurationManager.ConnectionStrings["CourseRegProDb"].ConnectionString;

        public static MySqlConnection CreateConnection()
        {
            return new MySqlConnection(ConnectionString);
        }
    }
}
