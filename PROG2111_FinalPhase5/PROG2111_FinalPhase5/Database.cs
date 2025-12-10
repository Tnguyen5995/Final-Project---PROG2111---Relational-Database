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
		public Database()
		{
            ds.Tables.Add(studentTable);
            ds.Tables.Add(programTable);
            ds.Tables.Add(courseTable);
            ds.Tables.Add(programCourseTable);
            ds.Tables.Add(instructorTable);
            ds.Tables.Add(CourseOfferingTable);
            ds.Tables.Add(CourseEnrollmentTable);
            ds.Tables.Add(InstructorAssignmentTable);

            StudentProgramIDRelation = new DataRelation("FK_Student_ProgramId", programTable.Columns["programId"], studentTable.Columns["studentId"]);
            ProgramCourseProgramIDRelation = new DataRelation("FK_ProgramCourse_ProgramId", programTable.Columns["programId"], programCourseTable.Columns["programId"]);
            ProgramCourseCourseIDRelation = new DataRelation("FK_ProgramCourse_CourseId", courseTable.Columns["courseId"], programCourseTable.Columns["courseId"]);
            CourseOfferingCourseIdRelation = new DataRelation("FK_CourseOffering_CourseId", courseTable.Columns["courseId"], CourseOfferingTable.Columns["courseId"]);
            CourseEnrollmentStudentIdRelation = new DataRelation("FK_CourseEnrollment_StudentId", studentTable.Columns["studentId"], CourseEnrollmentTable.Columns["studentId"]);
            CourseEnrollmentOfferingIdRelation = new DataRelation("FK_CourseEnrollment_OfferingId", CourseOfferingTable.Columns["offeringId"], CourseEnrollmentTable.Columns["offeringId"]);
            InstructorAssignmentInstructorIdRelation = new DataRelation("FK_InstructorAssignment_InstructorId", instructorTable.Columns["instructorId"], InstructorAssignmentTable.Columns["instructorId"]);
            InstructorAssignmentOfferingIdRelation = new DataRelation("FK_InstructorAssignment_OfferingId", CourseOfferingTable.Columns["offeringId"], InstructorAssignmentTable.Columns["offeringId"]);

            _programRepository = new ProgramRepository();
            _studentRepository = new StudentRepository();

            RefreshProgramTable();
            RefreshStudentTable();
        }

        public DataSet ds = new DataSet("DataSet");

        public DataRelation StudentProgramIDRelation;
        public DataRelation ProgramCourseProgramIDRelation;
        public DataRelation ProgramCourseCourseIDRelation;
        public DataRelation CourseOfferingCourseIdRelation;
        public DataRelation CourseEnrollmentStudentIdRelation;
        public DataRelation CourseEnrollmentOfferingIdRelation;
        public DataRelation InstructorAssignmentInstructorIdRelation;
        public DataRelation InstructorAssignmentOfferingIdRelation;

        public DataTable studentTable = new StudentTable().StudentDataTable;
        public DataTable programTable = new ProgramTable().ProgramDataTable;
        public DataTable courseTable = new CourseTable().CourseDataTable;
        public DataTable programCourseTable = new ProgramCourseTable().ProgramCourseDataTable;
        public DataTable instructorTable = new InstructorTable().InstructorDataTable;
        public DataTable CourseOfferingTable = new CourseOfferingTable().CourseOfferingDataTable;
        public DataTable CourseEnrollmentTable = new CourseEnrollmentTable().CourseEnrollmentDataTable;
        public DataTable InstructorAssignmentTable = new InstructorAssignmentTable().InstructorAssignemntDataTable;

        private readonly ProgramRepository _programRepository;
        private readonly StudentRepository _studentRepository;

        // --- Load / Refresh ---

        public void RefreshProgramTable()
        {
            programTable = _programRepository.GetAllProgramsAsDataTable();
        }

        public void RefreshStudentTable()
        {
            studentTable = _studentRepository.GetAllStudentsAsDataTable();
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
    }//end of Database
}//end of PROG2111_FinalPhase5
