using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROG2111_FinalPhase5
{
    /*
	 * FILE : InstructorTable.cs
	 * PROJECT : $safeprojectname$
	 * PROGRAMMER : George Shapka
	 * FIRST VERSION : 12/9/2025 3:34:16 PM
	 */
    internal class InstructorTable
    {
		public InstructorTable()
		{
			DataColumn instructorId = new DataColumn("instructorId", typeof(int));
			InstructorDataTable.Columns.Add(instructorId);
			InstructorDataTable.PrimaryKey = new DataColumn[] { instructorId };
			InstructorDataTable.Columns.Add("firstName", typeof(string));
			InstructorDataTable.Columns.Add("lastName", typeof(string));
			InstructorDataTable.Columns.Add("email", typeof(string));
			InstructorDataTable.Columns.Add("hireDate", typeof(DateTime));
            InstructorDataTable.Columns.Add("officeLoaction", typeof(string));
        }

        public DataTable InstructorDataTable = new DataTable("InstructorTable");
		public static List<int> InstructorIds = new List<int>();
    }//end of InstructorTable

}//end of PROG2111_FinalPhase5