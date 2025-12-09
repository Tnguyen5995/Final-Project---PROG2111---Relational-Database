using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROG2111_FinalPhase5
{
    /*
	 * FILE : InstructorAssignmentTable.cs
	 * PROJECT : $safeprojectname$
	 * PROGRAMMER : George Shapka
	 * FIRST VERSION : 12/9/2025 6:07:08 PM
	 */
    internal class InstructorAssignmentTable
    {
		public InstructorAssignmentTable()
		{
			DataColumn instructorId = new DataColumn("instructorId", typeof(int));
			InstructorAssignemntDataTable.Columns.Add(instructorId);
			DataColumn offeringId = new DataColumn("offeringId", typeof(int));
			InstructorAssignemntDataTable.Columns.Add(offeringId);

			InstructorAssignemntDataTable.PrimaryKey = new DataColumn[] { instructorId, offeringId };
		}

		public DataTable InstructorAssignemntDataTable = new DataTable("InstructorAssignmentTable");
    }//end of InstructorAssignmentTable

}//end of PROG2111_FinalPhase5