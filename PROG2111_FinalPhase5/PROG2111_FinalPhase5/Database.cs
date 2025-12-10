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
     *   Provides a facade over ProgramRepository and StudentRepository.
     *   Exposes in-memory DataTables for WPF binding and performs CRUD
     *   operations against the MySQL CourseRegProDB database, including
     *   a transactional delete for students and their enrollments.
     */
    internal class Database
    {
        // Repositories
        private readonly ProgramRepository _programRepository;
        private readonly StudentRepository _studentRepository;

        // Table wrappers
        private readonly StudentTable _studentTableWrapper;
        private readonly ProgramTable _programTableWrapper;
        private readonly CourseTable _courseTableWrapper;
        private readonly ProgramCourseTable _programCourseTableWrapper;
        private readonly InstructorTable _instructorTableWrapper;
        private readonly CourseOfferingTable _courseOfferingTableWrapper;
        private readonly CourseEnrollmentTable _courseEnrollmentTableWrapper;
        private readonly InstructorAssignmentTable _instructorAssignmentTableWrapper;

        // DataTables exposed to the UI
        public DataTable studentTable { get; }
        public DataTable programTable { get; }
        public DataTable courseTable { get; }
        public DataTable programCourseTable { get; }
        public DataTable instructorTable { get; }
        public DataTable CourseOfferingTable { get; }
        public DataTable CourseEnrollmentTable { get; }
        public DataTable InstructorAssignmentTable { get; }

        public Database()
        {
            _programRepository = new ProgramRepository();
            _studentRepository = new StudentRepository();

            _studentTableWrapper = new StudentTable();
            _programTableWrapper = new ProgramTable();
            _courseTableWrapper = new CourseTable();
            _programCourseTableWrapper = new ProgramCourseTable();
            _instructorTableWrapper = new InstructorTable();
            _courseOfferingTableWrapper = new CourseOfferingTable();
            _courseEnrollmentTableWrapper = new CourseEnrollmentTable();
            _instructorAssignmentTableWrapper = new InstructorAssignmentTable();

            studentTable = _studentTableWrapper.StudentDataTable;
            programTable = _programTableWrapper.ProgramDataTable;
            courseTable = _courseTableWrapper.CourseDataTable;
            programCourseTable = _programCourseTableWrapper.ProgramCourseDataTable;
            instructorTable = _instructorTableWrapper.InstructorDataTable;
            CourseOfferingTable = _courseOfferingTableWrapper.CourseOfferingDataTable;
            CourseEnrollmentTable = _courseEnrollmentTableWrapper.CourseEnrollmentDataTable;
            InstructorAssignmentTable = _instructorAssignmentTableWrapper.InstructorAssignemntDataTable;

            // Initial load from the database for Program and Student
            RefreshProgramTable();
            RefreshStudentTable();
        }

        // --------------------------------------------------------------------
        // Refresh methods (DB -> DataTable) for Program & Student
        // --------------------------------------------------------------------
        public void RefreshProgramTable()
        {
            programTable.Clear();

            DataTable latest = _programRepository.GetAllProgramsAsDataTable();
            foreach (DataRow row in latest.Rows)
            {
                programTable.ImportRow(row);
            }
        }

        public void RefreshStudentTable()
        {
            studentTable.Clear();

            DataTable latest = _studentRepository.GetAllStudentsAsDataTable();
            foreach (DataRow row in latest.Rows)
            {
                studentTable.ImportRow(row);
            }
        }

        // --------------------------------------------------------------------
        // Program CRUD
        // --------------------------------------------------------------------
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

        // --------------------------------------------------------------------
        // Student CRUD
        // --------------------------------------------------------------------
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

        /// <summary>
        /// Deletes a student and any related CourseEnrollment rows in a single
        /// MySQL transaction. This is our explicit demonstration of transaction
        /// control for Phase 5.
        /// </summary>
        public void DeleteStudent(int studentId)
        {
            using (MySqlConnection connection = DbConnectionFactory.CreateConnection())
            {
                connection.Open();

                using (MySqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // 1. Delete enrollments for this student (if any)
                        using (MySqlCommand deleteEnrollments = new MySqlCommand(
                                   @"DELETE FROM CourseEnrollment
                                     WHERE studentID = @studentID;",
                                   connection,
                                   transaction))
                        {
                            deleteEnrollments.Parameters.AddWithValue("@studentID", studentId);
                            deleteEnrollments.ExecuteNonQuery();
                        }

                        // 2. Delete the student record
                        using (MySqlCommand deleteStudent = new MySqlCommand(
                                   @"DELETE FROM Student
                                     WHERE studentID = @studentID;",
                                   connection,
                                   transaction))
                        {
                            deleteStudent.Parameters.AddWithValue("@studentID", studentId);

                            int rowsAffected = deleteStudent.ExecuteNonQuery();
                            if (rowsAffected == 0)
                            {
                                throw new InvalidOperationException(
                                    "No student record exists with the specified ID.");
                            }
                        }

                        // 3. Commit if both operations succeed
                        transaction.Commit();
                    }
                    catch
                    {
                        // Any failure -> rollback the transaction
                        transaction.Rollback();
                        throw;
                    }
                }
            }

            // Reload the student table from the DB
            RefreshStudentTable();
        }
    }
}
