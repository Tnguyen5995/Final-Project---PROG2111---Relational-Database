using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;

namespace PROG2111_FinalPhase5
{
    /*
     * FILE : StudentRepository.cs
     * PROJECT : PROG2111_FinalPhase5
     * PROGRAMMER : Tuan Thanh Nguyen
     * FIRST VERSION : 12/08/2025
     */
    internal class StudentRepository
    {
        public DataTable GetAllStudentsAsDataTable()
        {
            DataTable table = new DataTable();

            using (MySqlConnection connection = DbConnectionFactory.CreateConnection())
            {
                connection.Open();

                const string query =
                    @"SELECT studentID, programID, firstName, lastName, emailAddress,
                             dateOfBirth, dateEnrolled
                      FROM Student
                      ORDER BY lastName, firstName;";

                using (MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection))
                {
                    adapter.Fill(table);
                }
            }

            return table;
        }

        public List<StudentModel> GetAllStudents()
        {
            List<StudentModel> students = new List<StudentModel>();

            using (MySqlConnection connection = DbConnectionFactory.CreateConnection())
            {
                connection.Open();

                const string query =
                    @"SELECT studentID, programID, firstName, lastName, emailAddress,
                             dateOfBirth, dateEnrolled
                      FROM Student
                      ORDER BY lastName, firstName;";

                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        StudentModel model = new StudentModel
                        {
                            StudentId = reader.GetInt32("studentID"),
                            ProgramId = reader.GetInt32("programID"),
                            FirstName = reader.GetString("firstName"),
                            LastName = reader.GetString("lastName"),
                            EmailAddress = reader.GetString("emailAddress"),
                            DateOfBirth = reader.IsDBNull(reader.GetOrdinal("dateOfBirth"))
                                ? (DateTime?)null
                                : reader.GetDateTime("dateOfBirth"),
                            DateEnrolled = reader.GetDateTime("dateEnrolled")
                        };

                        students.Add(model);
                    }
                }
            }

            return students;
        }

        public StudentModel GetStudentById(int studentId)
        {
            using (MySqlConnection connection = DbConnectionFactory.CreateConnection())
            {
                connection.Open();

                const string query =
                    @"SELECT studentID, programID, firstName, lastName, emailAddress,
                             dateOfBirth, dateEnrolled
                      FROM Student
                      WHERE studentID = @studentID;";

                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@studentID", studentId);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new StudentModel
                            {
                                StudentId = reader.GetInt32("studentID"),
                                ProgramId = reader.GetInt32("programID"),
                                FirstName = reader.GetString("firstName"),
                                LastName = reader.GetString("lastName"),
                                EmailAddress = reader.GetString("emailAddress"),
                                DateOfBirth = reader.IsDBNull(reader.GetOrdinal("dateOfBirth"))
                                    ? (DateTime?)null
                                    : reader.GetDateTime("dateOfBirth"),
                                DateEnrolled = reader.GetDateTime("dateEnrolled")
                            };
                        }
                    }
                }
            }

            return null;
        }

        public void InsertStudent(StudentModel student)
        {
            using (MySqlConnection connection = DbConnectionFactory.CreateConnection())
            {
                connection.Open();

                const string query =
                    @"INSERT INTO Student
                        (studentID, programID, firstName, lastName, emailAddress,
                         dateOfBirth, dateEnrolled)
                      VALUES
                        (@studentID, @programID, @firstName, @lastName, @emailAddress,
                         @dateOfBirth, @dateEnrolled);";

                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    // Same idea: studentID is AUTO_INCREMENT in DB, but we are letting the UI
                    // supply it to match your current design.
                    cmd.Parameters.AddWithValue("@studentID", student.StudentId);
                    cmd.Parameters.AddWithValue("@programID", student.ProgramId);
                    cmd.Parameters.AddWithValue("@firstName", student.FirstName);
                    cmd.Parameters.AddWithValue("@lastName", student.LastName);
                    cmd.Parameters.AddWithValue("@emailAddress", student.EmailAddress);

                    if (student.DateOfBirth.HasValue)
                    {
                        cmd.Parameters.AddWithValue("@dateOfBirth", student.DateOfBirth.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@dateOfBirth", DBNull.Value);
                    }

                    cmd.Parameters.AddWithValue("@dateEnrolled", student.DateEnrolled);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void UpdateStudent(StudentModel student)
        {
            using (MySqlConnection connection = DbConnectionFactory.CreateConnection())
            {
                connection.Open();

                const string query =
                    @"UPDATE Student
                      SET programID = @programID,
                          firstName = @firstName,
                          lastName = @lastName,
                          emailAddress = @emailAddress,
                          dateOfBirth = @dateOfBirth,
                          dateEnrolled = @dateEnrolled
                      WHERE studentID = @studentID;";

                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@studentID", student.StudentId);
                    cmd.Parameters.AddWithValue("@programID", student.ProgramId);
                    cmd.Parameters.AddWithValue("@firstName", student.FirstName);
                    cmd.Parameters.AddWithValue("@lastName", student.LastName);
                    cmd.Parameters.AddWithValue("@emailAddress", student.EmailAddress);

                    if (student.DateOfBirth.HasValue)
                    {
                        cmd.Parameters.AddWithValue("@dateOfBirth", student.DateOfBirth.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@dateOfBirth", DBNull.Value);
                    }

                    cmd.Parameters.AddWithValue("@dateEnrolled", student.DateEnrolled);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteStudent(int studentId)
        {
            using (MySqlConnection connection = DbConnectionFactory.CreateConnection())
            {
                connection.Open();

                const string query =
                    @"DELETE FROM Student
                      WHERE studentID = @studentID;";

                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@studentID", studentId);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
