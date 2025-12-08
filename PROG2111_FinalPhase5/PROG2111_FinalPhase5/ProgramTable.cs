using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROG2111_FinalPhase5
{
    /*
	 * FILE : ProgramTable.cs
	 * PROJECT : $safeprojectname$
	 * PROGRAMMER : George Shapka
	 * FIRST VERSION : 12/8/2025 5:01:14 PM
	 */
    internal class ProgramTable
    {
		public ProgramTable()
		{
			DataColumn programId = new DataColumn("programId", typeof(int));
            ProgramDataTable.Columns.Add(programId);
            ProgramDataTable.PrimaryKey = new DataColumn[] { programId };
            ProgramDataTable.Columns.Add("programName", typeof(string));
            ProgramDataTable.Columns.Add("credentialType", typeof(string));
            ProgramDataTable.Columns.Add("durationInTerms", typeof(int));
            ProgramDataTable.Columns.Add("isAvalible", typeof(bool));

        }

        public DataTable ProgramDataTable = new DataTable("ProgramTable");
        public static List<int> ProgramIds = new List<int>();
    }//end of ProgramTable

}//end of PROG2111_FinalPhase5