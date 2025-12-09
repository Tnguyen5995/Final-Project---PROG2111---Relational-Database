using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROG2111_FinalPhase5
{
    /*
	 * FILE : CourseTable.cs
	 * PROJECT : $safeprojectname$
	 * PROGRAMMER : George Shapka
	 * FIRST VERSION : 12/9/2025 12:22:38 AM
	 */
    internal class CourseTable
    {
        public CourseTable()
        {
            DataColumn courseId = new DataColumn("courseId", typeof(int));
            CourseDataTable.Columns.Add(courseId);
            CourseDataTable.PrimaryKey = new DataColumn[] { courseId };
            CourseDataTable.Columns.Add("courseTitle", typeof(string));
            CourseDataTable.Columns.Add("courseDescription", typeof(string));
            CourseDataTable.Columns.Add("courseHours", typeof(int));
        }


        public DataTable CourseDataTable = new DataTable("CourseTable");
        public static List<int> CourseIds = new List<int>();
    }//end of CourseTable

}//end of PROG2111_FinalPhase5