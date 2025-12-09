using System.Data;

namespace PROG2111_FinalPhase5
{
    /*
     * FILE : Database.cs
     * PROJECT : PROG2111_FinalPhase5
     * PROGRAMMER : George Shapka, Tuan Thanh Nguyen
     * FIRST VERSION : 12/08/2025
     * 
     * PURPOSE :
     *   Wrapper around repository classes used by the WPF UI to obtain
     *   DataTables for Student and Program.
     */
    internal class Database
    {
        public DataTable studentTable { get; private set; }
        public DataTable programTable { get; private set; }

        private readonly ProgramRepository _programRepository;
        private readonly StudentRepository _studentRepository;

        public Database()
        {
            _programRepository = new ProgramRepository();
            _studentRepository = new StudentRepository();

            RefreshProgramTable();
            RefreshStudentTable();
        }

        public void RefreshProgramTable()
        {
            programTable = _programRepository.GetAllProgramsAsDataTable();
        }

        public void RefreshStudentTable()
        {
            studentTable = _studentRepository.GetAllStudentsAsDataTable();
        }
    }
}
