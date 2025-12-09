using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROG2111_FinalPhase5
{
    /*
	 * FILE : ProgramCourseTable.cs
	 * PROJECT : $safeprojectname$
	 * PROGRAMMER : George Shapka
	 * FIRST VERSION : 12/9/2025 1:44:01 AM
	 */
    internal class ProgramCourseTable
    {
		public ProgramCourseTable()
		{
			DataColumn programId = new DataColumn("programId", typeof(int));
			ProgramCourseDataTable.Columns.Add(programId);
			DataColumn courseId = new DataColumn("courseId", typeof(int));
			ProgramCourseDataTable.Columns.Add(courseId);

			ProgramCourseDataTable.PrimaryKey = new DataColumn[] {programId, courseId};
		}

		public DataTable ProgramCourseDataTable = new DataTable("ProgramCourseTable");
    }//end of ProgramCourseTable

}//end of PROG2111_FinalPhase5