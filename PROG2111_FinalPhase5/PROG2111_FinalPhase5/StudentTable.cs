using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROG2111_FinalPhase5
{
    /*
	 * FILE : StudentTable.cs
	 * PROJECT : $safeprojectname$
	 * PROGRAMMER : George Shapka
	 * FIRST VERSION : 12/8/2025 2:19:52 PM
	 */
    internal class StudentTable
    {
		public StudentTable()
		{
            DataColumn studentId = new DataColumn("studentId", typeof(int));
            StudentDataTable.Columns.Add(studentId);
            StudentDataTable.PrimaryKey = new DataColumn[] { studentId };
            StudentDataTable.Columns.Add("programId", typeof(int));
            StudentDataTable.Columns.Add("firstName", typeof(string));
            StudentDataTable.Columns.Add("lastName", typeof(string));
            StudentDataTable.Columns.Add("email", typeof(string));
            StudentDataTable.Columns.Add("dateOfBirth", typeof(DateTime));
            StudentDataTable.Columns.Add("dateEnrolled", typeof(DateTime));

            //pre load stuff
            //StudentDataTable.Rows.Add(1, 1, "fred", "smith", "fred@mail.com", DateTime.Now, DateTime.UtcNow);
        }

        public DataTable StudentDataTable = new DataTable("StudentTable");
        public static List<int> StudentIds = new List<int>();
    }//end of StudentTable

}//end of PROG2111_FinalPhase5