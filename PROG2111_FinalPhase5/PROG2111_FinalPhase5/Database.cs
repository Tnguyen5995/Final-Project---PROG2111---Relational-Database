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
            StudentProgramRelation = new DataRelation("FK_Program_Student", programTable.Columns["programId"], studentTable.Columns["studentId"]);
            ds.Tables.Add(courseTable);
        }
        public DataRelation StudentProgramRelation;
        public DataSet ds = new DataSet("DataSet");
        public DataTable studentTable = new StudentTable().StudentDataTable;
        public DataTable programTable = new ProgramTable().ProgramDataTable;
        public DataTable courseTable = new CourseTable().CourseDataTable;
    }//end of Database

}//end of PROG2111_FinalPhase5