using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;

namespace PROG2111_FinalPhase5
{
    /*
     * FILE : ProgramRepository.cs
     * PROJECT : PROG2111_FinalPhase5
     * PROGRAMMER : Tuan Thanh Nguyen
     * FIRST VERSION : 12/08/2025
     */
    internal class ProgramRepository
    {
        /// <summary>
        /// Returns all programs as a DataTable, with columns matching ProgramTable:
        /// programId, programName, credentialType, durationInTerms, isAvalible
        /// </summary>
        public DataTable GetAllProgramsAsDataTable()
        {
            DataTable dt = new DataTable();

            using (MySqlConnection connection = DbConnectionFactory.CreateConnection())
            {
                connection.Open();

                const string query = @"
                    SELECT
                        programID       AS programId,
                        programName,
                        credentialType,
                        durationInTerms,
                        isAvailable     AS isAvalible
                    FROM Program
                    ORDER BY programName;";

                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                {
                    adapter.Fill(dt);
                }
            }

            return dt;
        }

        public List<ProgramModel> GetAllPrograms()
        {
            List<ProgramModel> programs = new List<ProgramModel>();

            using (MySqlConnection connection = DbConnectionFactory.CreateConnection())
            {
                connection.Open();

                const string query = @"
                    SELECT
                        programID,
                        programName,
                        credentialType,
                        durationInTerms,
                        isAvailable
                    FROM Program
                    ORDER BY programName;";

                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        programs.Add(new ProgramModel
                        {
                            ProgramId = reader.GetInt32("programID"),
                            ProgramName = reader.GetString("programName"),
                            CredentialType = reader.GetString("credentialType"),
                            DurationInTerms = Convert.ToByte(reader.GetByte("durationInTerms")),
                            IsAvailable = reader.GetBoolean("isAvailable")
                        });
                    }
                }
            }

            return programs;
        }

        public ProgramModel GetProgramById(int programId)
        {
            using (MySqlConnection connection = DbConnectionFactory.CreateConnection())
            {
                connection.Open();

                const string query = @"
                    SELECT
                        programID,
                        programName,
                        credentialType,
                        durationInTerms,
                        isAvailable
                    FROM Program
                    WHERE programID = @programID;";

                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@programID", programId);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new ProgramModel
                            {
                                ProgramId = reader.GetInt32("programID"),
                                ProgramName = reader.GetString("programName"),
                                CredentialType = reader.GetString("credentialType"),
                                DurationInTerms = Convert.ToByte(reader.GetByte("durationInTerms")),
                                IsAvailable = reader.GetBoolean("isAvailable")
                            };
                        }
                    }
                }
            }

            return null;
        }

        public void InsertProgram(ProgramModel program)
        {
            using (MySqlConnection connection = DbConnectionFactory.CreateConnection())
            {
                connection.Open();

                const string query = @"
                    INSERT INTO Program
                        (programID, programName, credentialType, durationInTerms, isAvailable)
                    VALUES
                        (@programID, @programName, @credentialType, @durationInTerms, @isAvailable);";

                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@programID", program.ProgramId);
                    cmd.Parameters.AddWithValue("@programName", program.ProgramName);
                    cmd.Parameters.AddWithValue("@credentialType", program.CredentialType);
                    cmd.Parameters.AddWithValue("@durationInTerms", program.DurationInTerms);
                    cmd.Parameters.AddWithValue("@isAvailable", program.IsAvailable);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void UpdateProgram(ProgramModel program)
        {
            using (MySqlConnection connection = DbConnectionFactory.CreateConnection())
            {
                connection.Open();

                const string query = @"
                    UPDATE Program
                    SET
                        programName     = @programName,
                        credentialType  = @credentialType,
                        durationInTerms = @durationInTerms,
                        isAvailable     = @isAvailable
                    WHERE programID   = @programID;";

                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@programID", program.ProgramId);
                    cmd.Parameters.AddWithValue("@programName", program.ProgramName);
                    cmd.Parameters.AddWithValue("@credentialType", program.CredentialType);
                    cmd.Parameters.AddWithValue("@durationInTerms", program.DurationInTerms);
                    cmd.Parameters.AddWithValue("@isAvailable", program.IsAvailable);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteProgram(int programId)
        {
            using (MySqlConnection connection = DbConnectionFactory.CreateConnection())
            {
                connection.Open();

                const string query = @"
                    DELETE FROM Program
                    WHERE programID = @programID;";

                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@programID", programId);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
