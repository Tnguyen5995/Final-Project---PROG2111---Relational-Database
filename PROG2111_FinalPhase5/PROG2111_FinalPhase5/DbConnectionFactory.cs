using System.Configuration;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace PROG2111_FinalPhase5
{
    /*
     * FILE : DbConnectionFactory.cs
     * PROJECT : PROG2111_FinalPhase5
     * PROGRAMMER : Tuan Thanh Nguyen
     * FIRST VERSION : 12/08/2025
     */
    public static class DbConnectionFactory
    {
        public static MySqlConnection CreateConnection()
        {
            string connectionString =
                ConfigurationManager.ConnectionStrings["CourseRegProDb"].ConnectionString;

            return new MySqlConnection(connectionString);
        }
    }
}
