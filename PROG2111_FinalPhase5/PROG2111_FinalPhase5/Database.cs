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

            StudentProgramIDRelation = new DataRelation("FK_Student_ProgramId", programTable.Columns["programId"], studentTable.Columns["studentId"]);
            ProgramCourseProgramIDRelation = new DataRelation("FK_ProgramCourse_ProgramId", programTable.Columns["programId"], programCourseTable.Columns["programId"]);
            ProgramCourseCourseIDRelation = new DataRelation("FK_ProgramCourse_CourseId", courseTable.Columns["courseId"], programCourseTable.Columns["courseId"]);
        }
        public DataRelation StudentProgramIDRelation;
        public DataRelation ProgramCourseProgramIDRelation;
        public DataRelation ProgramCourseCourseIDRelation;

        public DataSet ds = new DataSet("DataSet");

        public DataTable studentTable = new StudentTable().StudentDataTable;
        public DataTable programTable = new ProgramTable().ProgramDataTable;
        public DataTable courseTable = new CourseTable().CourseDataTable;
        public DataTable programCourseTable = new ProgramCourseTable().ProgramCourseDataTable;
    }//end of Database

        public void RefreshStudentTable()
        {
            studentTable = _studentRepository.GetAllStudentsAsDataTable();
        }
    }
}
