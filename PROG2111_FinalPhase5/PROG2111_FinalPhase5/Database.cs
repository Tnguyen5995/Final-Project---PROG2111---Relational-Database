using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROG2111_FinalPhase5
{
    /*
	 * FILE : Database.cs
	 * PROJECT : $safeprojectname$
	 * PROGRAMMER : George Shapka
	 * FIRST VERSION : 12/8/2025 2:17:53 PM
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
    }//end of Database

}//end of PROG2111_FinalPhase5