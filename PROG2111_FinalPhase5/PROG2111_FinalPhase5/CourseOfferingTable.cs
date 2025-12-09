using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROG2111_FinalPhase5
{
    /*
	 * FILE : CourseOfferingTable.cs
	 * PROJECT : $safeprojectname$
	 * PROGRAMMER : George Shapka
	 * FIRST VERSION : 12/9/2025 3:47:31 PM
	 */
    internal class CourseOfferingTable
    {
		public CourseOfferingTable()
		{
			DataColumn offeringId = new DataColumn("offeringId", typeof(int));
			CourseOfferingDataTable.Columns.Add(offeringId);
			CourseOfferingDataTable.PrimaryKey = new DataColumn[] { offeringId };

			CourseOfferingDataTable.Columns.Add("courseId", typeof(int));
            CourseOfferingDataTable.Columns.Add("instructorId", typeof(int));
            CourseOfferingDataTable.Columns.Add("termStart", typeof(int));
			CourseOfferingDataTable.Columns.Add("termEnd", typeof(int));
			CourseOfferingDataTable.Columns.Add("acedemicYear", typeof(int));
			CourseOfferingDataTable.Columns.Add("scheduleInfo", typeof(string));
			CourseOfferingDataTable.Columns.Add("selectionCode", typeof(int));
			CourseOfferingDataTable.Columns.Add("deliveryMode", typeof(string));
			CourseOfferingDataTable.Columns.Add("maxCapacity", typeof(int));
			CourseOfferingDataTable.Columns.Add("roomLocation", typeof(string));
		}
		
		public DataTable CourseOfferingDataTable = new DataTable("CourseOfferingTable");
		public static List<int> OfferingIds = new List<int>();
    }//end of CourseOfferingTable

}//end of PROG2111_FinalPhase5