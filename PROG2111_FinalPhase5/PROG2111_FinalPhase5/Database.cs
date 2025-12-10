using System;
using System.Data;
using MySql.Data.MySqlClient;


namespace PROG2111_FinalPhase5
{
    /*
     * FILE : Database.cs
     * PROJECT : PROG2111_FinalPhase5
     * PROGRAMMER : George Shapka, Tuan Thanh Nguyen
     * FIRST VERSION : 12/09/2025
     *
     * PURPOSE :
     *   Provides a simple facade over ProgramRepository and StudentRepository.
     *   The WPF UI uses this class to load DataTables for display and to perform
     *   CRUD operations against the MySQL CourseRegProDB database.
     */
    internal class Database
    {
        // Main tables used by the UI
        public DataTable StudentTable { get; private set; }
        public DataTable ProgramTable { get; private set; }

        // Compatibility properties – your existing UI uses lower-case names
        public DataTable studentTable => StudentTable;
        public DataTable programTable => ProgramTable;

        private readonly ProgramRepository _programRepository;
        private readonly StudentRepository _studentRepository;

        public Database()
        {
            _programRepository = new ProgramRepository();
            _studentRepository = new StudentRepository();

            RefreshProgramTable();
            RefreshStudentTable();
        }

        // --- Load / Refresh ---

        public void RefreshProgramTable()
        {
            ProgramTable = _programRepository.GetAllProgramsAsDataTable();
        }

        public void RefreshStudentTable()
        {
            StudentTable = _studentRepository.GetAllStudentsAsDataTable();
        }

        // --- Program CRUD ----

        public void InsertProgram(ProgramModel program)
        {
            _programRepository.InsertProgram(program);
            RefreshProgramTable();
        }

        public void UpdateProgram(ProgramModel program)
        {
            _programRepository.UpdateProgram(program);
            RefreshProgramTable();
        }

        public void DeleteProgram(int programId)
        {
            _programRepository.DeleteProgram(programId);
            RefreshProgramTable();
        }

        // --- Student CRUD ---

        public void InsertStudent(StudentModel student)
        {
            _studentRepository.InsertStudent(student);
            RefreshStudentTable();
        }

        public void UpdateStudent(StudentModel student)
        {
            _studentRepository.UpdateStudent(student);
            RefreshStudentTable();
        }

        public void DeleteStudent(int studentId)
        {
            using (MySqlConnection connection = DbConnectionFactory.CreateConnection())
            {
                connection.Open();

                using (MySqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // First, delete any enrollments for this student
                        using (MySqlCommand deleteEnrollmentsCommand = new MySqlCommand(
                                   @"DELETE FROM CourseEnrollment 
                             WHERE studentID = @studentID;",
                                   connection,
                                   transaction))
                        {
                            deleteEnrollmentsCommand.Parameters.Add("@studentID", MySqlDbType.Int32).Value = studentId;
                            deleteEnrollmentsCommand.ExecuteNonQuery();
                        }

                        // Then, delete the student record itself
                        using (MySqlCommand deleteStudentCommand = new MySqlCommand(
                                   @"DELETE FROM Student 
                             WHERE studentID = @studentID;",
                                   connection,
                                   transaction))
                        {
                            deleteStudentCommand.Parameters.Add("@studentID", MySqlDbType.Int32).Value = studentId;

                            int rowsAffected = deleteStudentCommand.ExecuteNonQuery();

                            if (rowsAffected == 0)
                            {
                                // No row = no such student – treat as error for the caller
                                throw new InvalidOperationException(
                                    "No student record exists with the specified ID.");
                            }
                        }

                        // Everything succeeded – commit the transaction
                        transaction.Commit();

                        // Refresh the in-memory DataTable so the UI stays in sync
                        this.RefreshStudentsFromDatabase();
                    }
                    catch (MySqlException ex)
                    {
                        transaction.Rollback();
                        throw new ApplicationException(
                            "An error occurred while deleting the student and enrollments from the database.",
                            ex);
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        private void RefreshStudentsFromDatabase()
        {
            throw new NotImplementedException();
        }
    }
}
