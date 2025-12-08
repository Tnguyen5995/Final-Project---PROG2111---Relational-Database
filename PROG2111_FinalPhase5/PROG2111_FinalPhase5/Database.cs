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
        }

        public DataTable studentTable = new StudentTable().StudentDataTable;
        public DataSet ds = new DataSet("DataSet");
    }//end of Database

}//end of PROG2111_FinalPhase5