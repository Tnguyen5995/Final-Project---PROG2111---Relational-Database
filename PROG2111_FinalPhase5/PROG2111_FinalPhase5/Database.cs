using System.Data;

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
            _studentRepository.DeleteStudent(studentId);
            RefreshStudentTable();
        }
    }
}
