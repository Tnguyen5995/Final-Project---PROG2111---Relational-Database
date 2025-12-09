using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROG2111_FinalPhase5
{
    /*
	 * FILE : CourseEnrollmentTable.cs
	 * PROJECT : $safeprojectname$
	 * PROGRAMMER : George Shapka
	 * FIRST VERSION : 12/9/2025 4:22:15 PM
	 */
    internal class CourseEnrollmentTable
    {
		public CourseEnrollmentTable()
		{
			DataColumn studentId = new DataColumn("studentId", typeof(int));
			CourseEnrollmentDataTable.Columns.Add(studentId);
			DataColumn offeringId = new DataColumn("offeringId", typeof(int));
			CourseEnrollmentDataTable.Columns.Add(offeringId);
			CourseEnrollmentDataTable.PrimaryKey = new DataColumn[] {studentId, offeringId};

			CourseEnrollmentDataTable.Columns.Add("enrollmentStatus", typeof(string));
			CourseEnrollmentDataTable.Columns.Add("finalGrade", typeof(float));
		}

		public DataTable CourseEnrollmentDataTable = new DataTable("CourseEnrollmentTable");
    }//end of CourseEnrollmentTable

}//end of PROG2111_FinalPhase5