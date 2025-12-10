using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using MySql.Data.MySqlClient;

namespace PROG2111_FinalPhase5
{
	/*
	 * FILE : MainWindow_CreateTab.cs
	 * PROJECT : $safeprojectname$
	 * PROGRAMMER : George Shapka, Tuan Thanh Nguyen
	 * FIRST VERSION : 12/8/2025 2:06:16 PM
	 */
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private void createComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			SetAllTablesInvisible();
			string selection = createComboBox.SelectedItem.ToString();
			selection = selection.Remove(0, ComboBoxString.Length);
			//MessageBox.Show(selection);

			switch (selection)
			{
				case "Student Table":
					createStudentGrid.Visibility = Visibility.Visible;
					break;
				case "Program Table":
					createProgramGrid.Visibility = Visibility.Visible; 
					break;
				case "Course Table":
					createCourseGrid.Visibility = Visibility.Visible;
					break;
				case "Program Course Table":
					createProgramCourseGrid.Visibility = Visibility.Visible;
					break;
				case "Instructor Table":
					createInstructorGrid.Visibility = Visibility.Visible;
					break;
				case "Course Offering Table":
                    createCourseOfferingGrid.Visibility = Visibility.Visible;
					break;
				case "Course Enrollment Table":
                    createCourseEnrollmentGrid.Visibility = Visibility.Visible;
					break;
				case "Instructor Assignment Table":
                    createInstructorAssignmentGrid.Visibility = Visibility.Visible;
					break;
			}
		}
        private void btnStudentSubmit_Click(object sender, RoutedEventArgs e)
        {
            bool error = false;

            // student id
            int studentID;
            txtCreateStudentID.Background = Brushes.Transparent;
            if (!int.TryParse(txtCreateStudentID.Text, out studentID))
            {
                txtCreateStudentID.Background = Brushes.Red;
                error = true;
            }

            // program id
            int studentProgramId;
            txtCreateStudentProgramID.Background = Brushes.Transparent;
            if (!int.TryParse(txtCreateStudentProgramID.Text, out studentProgramId))
            {
                txtCreateStudentProgramID.Background = Brushes.Red;
                error = true;
            }

            // first name
            txtCreateStudentFirstName.Background = Brushes.Transparent;
            if (txtCreateStudentFirstName.Text.Trim().Length == 0)
            {
                txtCreateStudentFirstName.Background = Brushes.Red;
                error = true;
            }

            // last name
            txtCreateStudentLastName.Background = Brushes.Transparent;
            if (txtCreateStudentLastName.Text.Trim().Length == 0)
            {
                txtCreateStudentLastName.Background = Brushes.Red;
                error = true;
            }

            // email
            txtCreateStudentEmail.Background = Brushes.Transparent;
            if (txtCreateStudentEmail.Text.Trim().Length == 0)
            {
                txtCreateStudentEmail.Background = Brushes.Red;
                error = true;
            }

            // date of birth
            DateTime dateOfBirth;
            dateCreateStudentDateOfBirth.Foreground = Brushes.Black;
            if (!DateTime.TryParse(dateCreateStudentDateOfBirth.Text, out dateOfBirth))
            {
                dateCreateStudentDateOfBirth.Foreground = Brushes.Red;
                error = true;
            }

            // date enrolled
            DateTime dateEnrolled;
            dateCreateStudentDateEnrolled.Foreground = Brushes.Black;
            if (!DateTime.TryParse(dateCreateStudentDateEnrolled.Text, out dateEnrolled))
            {
                dateCreateStudentDateEnrolled.Foreground = Brushes.Red;
                error = true;
            }

            if (error)
            {
                return;
            }

            try
            {
                StudentModel student = new StudentModel
                {
                    StudentId = studentID,
                    ProgramId = studentProgramId,
                    FirstName = txtCreateStudentFirstName.Text.Trim(),
                    LastName = txtCreateStudentLastName.Text.Trim(),
                    EmailAddress = txtCreateStudentEmail.Text.Trim(),
                    DateOfBirth = dateOfBirth,
                    DateEnrolled = dateEnrolled
                };

                db.InsertStudent(student);

                MessageBox.Show("Student saved to database.", "Success",
                    MessageBoxButton.OK, MessageBoxImage.Information);

                // clear fields
                txtCreateStudentID.Text = string.Empty;
                txtCreateStudentProgramID.Text = string.Empty;
                txtCreateStudentFirstName.Text = string.Empty;
                txtCreateStudentLastName.Text = string.Empty;
                txtCreateStudentEmail.Text = string.Empty;
                dateCreateStudentDateOfBirth.Text = string.Empty;
                dateCreateStudentDateEnrolled.Text = string.Empty;

                RefreshReadGrids();
            }
            catch (MySqlException dbEx)
            {
                MessageBox.Show("Database error while saving student:\n" + dbEx.Message,
                    "Database Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unexpected error while saving student:\n" + ex.Message,
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnProgramSubmit_Click(object sender, RoutedEventArgs e)
        {
            bool error = false;

            // program id
            int programId;
            txtCreateProgramID.Background = Brushes.Transparent;
            if (!int.TryParse(txtCreateProgramID.Text, out programId))
            {
                txtCreateProgramID.Background = Brushes.Red;
                error = true;
            }

            // name
            txtCreateProgramName.Background = Brushes.Transparent;
            if (txtCreateProgramName.Text.Trim().Length == 0)
            {
                txtCreateProgramName.Background = Brushes.Red;
                error = true;
            }

            // credential
            txtCreateProgramCredential.Background = Brushes.Transparent;
            if (txtCreateProgramCredential.Text.Trim().Length == 0)
            {
                txtCreateProgramCredential.Background = Brushes.Red;
                error = true;
            }

            // duration
            int duration;
            txtCreateProgramDuration.Background = Brushes.Transparent;
            if (!int.TryParse(txtCreateProgramDuration.Text, out duration))
            {
                txtCreateProgramDuration.Background = Brushes.Red;
                error = true;
            }

            // availability checkbox
            bool isAvailable = chkCreateProgramAvaliblility.IsChecked == true;

            if (error)
            {
                return;
            }

            try
            {
                ProgramModel program = new ProgramModel
                {
                    ProgramId = programId,
                    ProgramName = txtCreateProgramName.Text.Trim(),
                    CredentialType = txtCreateProgramCredential.Text.Trim(),
                    DurationInTerms = (byte)duration,
                    IsAvailable = isAvailable
                };

                db.InsertProgram(program);

                MessageBox.Show("Program saved to database.", "Success",
                    MessageBoxButton.OK, MessageBoxImage.Information);

                // clear fields
                txtCreateProgramID.Text = string.Empty;
                txtCreateProgramName.Text = string.Empty;
                txtCreateProgramCredential.Text = string.Empty;
                txtCreateProgramDuration.Text = string.Empty;
                chkCreateProgramAvaliblility.IsChecked = false;

                RefreshReadGrids();
            }
            catch (MySqlException dbEx)
            {
                MessageBox.Show("Database error while saving program:\n" + dbEx.Message,
                    "Database Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unexpected error while saving program:\n" + ex.Message,
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void btnCreateCourseSubmit_Click(object sender, RoutedEventArgs e)
		{
			bool error = false;

			//id
			int courseId;
			txtCreateCourseID.Background = Brushes.Transparent;
			if (!int.TryParse(txtCreateCourseID.Text, out courseId))
			{
				txtCreateCourseID.Background = Brushes.Red;
				error = true;
			}
			if (CourseTable.CourseIds.Contains(courseId))
			{
				txtCreateCourseID.Text = "Id already exists";
				txtCreateCourseID.Background = Brushes.Red;
				error = true;
			}

			//title
			txtCreateCourseTitle.Background = Brushes.Transparent;
			if (!(txtCreateCourseTitle.Text.Length > 0))
			{
				txtCreateCourseTitle.Background = Brushes.Red;
				error = true;
			}

			//description
			txtCreateCourseDescription.Background = Brushes.Transparent;
			if (!(txtCreateCourseDescription.Text.Length > 0))
			{
				txtCreateCourseDescription.Background = Brushes.Red;
				error = true;
			}

			//duration
			int hours;
			txtCreateCourseHours.Background = Brushes.Transparent;
			if (!int.TryParse(txtCreateCourseHours.Text, out hours))
			{
				txtCreateCourseHours.Background = Brushes.Red;
				error = true;
			}

			if (error)
			{
				return;
			}

			CourseTable.CourseIds.Add(courseId);
			DataRow dr = db.courseTable.NewRow();

			dr[0] = courseId;
			dr[1] = txtCreateCourseTitle.Text;
			dr[2] = txtCreateCourseDescription.Text;
			dr[3] = hours;

			db.courseTable.Rows.Add(dr);

			txtCreateCourseID.Text = string.Empty;
			txtCreateCourseTitle.Text = string.Empty;
			txtCreateCourseDescription.Text = string.Empty;
			txtCreateCourseHours.Text = string.Empty;
		}
		private void btnCreateProgramCourseSubmit_Click(object sender, RoutedEventArgs e)
		{
			bool error = false;

			//program id
			int programId;
			txtCreateProgramCourseProgramID.Background = Brushes.Transparent;
			if (!int.TryParse(txtCreateProgramCourseProgramID.Text, out programId))
			{
				txtCreateProgramCourseProgramID.Background = Brushes.Red;
				error = true;
			}
			if (!ProgramTable.ProgramIds.Contains(programId))
			{
				txtCreateProgramCourseProgramID.Text = "Id doesnt exist";
				txtCreateProgramCourseProgramID.Background = Brushes.Red;
				error = true;
			}
			//course id
			int courseId;
			txtCreateProgramCourseCourseID.Background = Brushes.Transparent;
			if (!int.TryParse(txtCreateProgramCourseCourseID.Text, out courseId))
			{
				txtCreateProgramCourseCourseID.Background = Brushes.Red;
				error = true;
			}
			if (!CourseTable.CourseIds.Contains(courseId))
			{
				txtCreateProgramCourseCourseID.Text = "Id doesnt exist";
				txtCreateProgramCourseCourseID.Background = Brushes.Red;
				error = true;
			}
			int[] key = new int[] { programId, courseId };

            if (ProgramCourseTable.ProgramCourseKeys.Contains(key))
            {
                txtCreateProgramCourseProgramID.Background = Brushes.Red;
                txtCreateProgramCourseCourseID.Background = Brushes.Red;
                txtCreateProgramCourseCourseID.Text = "Program Course Already Exists";
                error = true;
            }

			if(error)
			{
                return;
			}

			DataRow dr = db.programCourseTable.NewRow();
			dr[0] = programId;
			dr[1] = courseId;

			db.programCourseTable.Rows.Add(dr);

			ProgramCourseTable.ProgramCourseKeys.Add(new int[] { programId, courseId });

			txtCreateProgramCourseProgramID.Text = string.Empty;
			txtCreateProgramCourseCourseID.Text = string.Empty;
		}

		private void btnCreateInstructorSubmit_Click(object sender, RoutedEventArgs e)
		{
			bool error = false;

			//program id
			int instructorId;
			txtCreateInstructorID.Background = Brushes.Transparent;
			if (!int.TryParse(txtCreateInstructorID.Text, out instructorId))
			{
				txtCreateInstructorID.Background = Brushes.Red;
				error = true;
			}
			if (ProgramTable.ProgramIds.Contains(instructorId))
			{
				txtCreateInstructorID.Text = "Id already exists";
				txtCreateInstructorID.Background = Brushes.Red;
				error = true;
			}
			//first name
			if(!(txtCreateInstructorFirstName.Text.Length > 0))
			{
				txtCreateInstructorFirstName.Background = Brushes.Red;
				error = true;
			}
			//last name
			if(!(txtCreateInstructorLastName.Text.Length > 0))
			{
				txtCreateInstructorLastName.Background = Brushes.Red;
				error = true;
			}
			//email
			if(!(txtCreateInstructorEmail.Text.Length > 0))
			{
				txtCreateInstructorEmail.Background = Brushes.Red;
				error = true;
			}
			//hire date
			DateTime hireDate;
			if(!DateTime.TryParse(dateCreateInstructorHireDate.Text, out hireDate))
			{
				dateCreateInstructorHireDate.Background = Brushes.Red;
				error = true;
			}
			//office location
			if(!(txtCreateInstructorOfficeLocation.Text.Length > 0))
			{
				txtCreateInstructorOfficeLocation.Background = Brushes.Red;
				error = true;
			}

			if (error)
			{
				return;
			}

			DataRow dr = db.instructorTable.NewRow();
			dr[0] = instructorId;
			dr[1] = txtCreateInstructorFirstName.Text;
			dr[2] = txtCreateInstructorLastName.Text;
			dr[3] = txtCreateInstructorEmail.Text;
			dr[4] = hireDate;
			dr[5] = txtCreateInstructorOfficeLocation.Text;

			db.instructorTable.Rows.Add(dr);

			txtCreateInstructorID.Text = string.Empty;
			txtCreateInstructorFirstName.Text = string.Empty;
			txtCreateInstructorLastName.Text = string.Empty;
			txtCreateInstructorEmail.Text = string.Empty;
			dateCreateInstructorHireDate.Text = string.Empty;
			txtCreateInstructorOfficeLocation.Text = string.Empty;
        }

        private void btnCourseOfferingSubmit_Click(object sender, RoutedEventArgs e)
        {
            bool error = false;

            //offering id
            int offeringId;
            txtCourseOfferingID.Background = Brushes.Transparent;
            if (!int.TryParse(txtCourseOfferingID.Text, out offeringId))
            {
                txtCourseOfferingID.Background = Brushes.Red;
                error = true;
            }
            if (CourseOfferingTable.OfferingIds.Contains(offeringId))
            {
                txtCourseOfferingID.Text = "Id already exists";
                txtCourseOfferingID.Background = Brushes.Red;
                error = true;
            }
            //course id
            int courseId;
            txtCourseOfferingCourseID.Background = Brushes.Transparent;
            if (!int.TryParse(txtCourseOfferingCourseID.Text, out courseId))
            {
                txtCourseOfferingCourseID.Background = Brushes.Red;
                error = true;
            }
            if (!CourseTable.CourseIds.Contains(courseId))
            {
                txtCourseOfferingCourseID.Text = "Id doesnt exist";
                txtCourseOfferingCourseID.Background = Brushes.Red;
                error = true;
            }
			//termStart
			int termStart;
            txtCourseOfferingTermStart.Background = Brushes.Transparent;
            if (!int.TryParse(txtCourseOfferingTermStart.Text, out termStart))
			{
				txtCourseOfferingTermStart.Background= Brushes.Red;
				error = true;
			}
			//termEnd
			int termEnd;
			txtCourseOfferingTermEnd.Background = Brushes.Transparent;
			if(!int.TryParse(txtCourseOfferingTermEnd.Text, out termEnd))
			{
				txtCourseOfferingTermEnd.Background= Brushes.Red;
				error = true;
			}
			//acedemicYear
			int acedemicYear;
			txtCourseOfferingAcedemicYear.Background = Brushes.Transparent;
			if(!int.TryParse(txtCourseOfferingAcedemicYear.Text, out acedemicYear))
			{
				txtCourseOfferingAcedemicYear.Background= Brushes.Red;
				error = true;
			}
            //scheduleInfo
            txtCourseOfferingScheduleInfo.Background = Brushes.Transparent;
            if (!(txtCourseOfferingScheduleInfo.Text.Length > 0))
            {
                txtCourseOfferingScheduleInfo.Background = Brushes.Red;
                error = true;
            }
			//selectionCode
			int selectionCode;
            txtCourseOfferingSelectionCode.Background = Brushes.Transparent;
            if (!int.TryParse(txtCourseOfferingSelectionCode.Text, out selectionCode))
			{
				txtCourseOfferingSelectionCode.Background = Brushes.Red;
				error = true;
			}
            //deliveryMode
            txtCourseOfferingDeliveryMode.Background = Brushes.Transparent;
            if (!(txtCourseOfferingDeliveryMode.Text.Length > 0))
            {
                txtCourseOfferingDeliveryMode.Background = Brushes.Red;
                error = true;
            }
            //maxCapacity
            int maxCapacity;
            txtCourseOfferingMaxCapacity.Background = Brushes.Transparent;
            if (!int.TryParse(txtCourseOfferingMaxCapacity.Text, out maxCapacity))
            {
                txtCourseOfferingMaxCapacity.Background = Brushes.Red;
                error = true;
            }
            //roomLocation
            txtCourseOfferingRoomLocation.Background = Brushes.Transparent;
            if (!(txtCourseOfferingRoomLocation.Text.Length > 0))
            {
                txtCourseOfferingRoomLocation.Background = Brushes.Red;
                error = true;
            }

            if (error)
            {
                return;
            }

            DataRow dr = db.CourseOfferingTable.NewRow();
            dr[0] = offeringId;
            dr[1] = courseId;
            dr[2] = termStart;
            dr[3] = termEnd;
            dr[4] = acedemicYear;
            dr[5] = txtCourseOfferingScheduleInfo.Text;
            dr[6] = selectionCode;
            dr[7] = txtCourseOfferingDeliveryMode.Text;
            dr[8] = maxCapacity;
            dr[9] = txtCourseOfferingRoomLocation.Text;

			db.CourseOfferingTable.Rows.Add(dr);

			txtCourseOfferingID.Text = string.Empty;
			txtCourseOfferingCourseID.Text = string.Empty;
			txtCourseOfferingTermStart.Text = string.Empty;
			txtCourseOfferingTermEnd.Text = string.Empty;
			txtCourseOfferingAcedemicYear.Text = string.Empty;
			txtCourseOfferingScheduleInfo.Text = string.Empty;
			txtCourseOfferingSelectionCode.Text = string.Empty;
			txtCourseOfferingDeliveryMode.Text = string.Empty;
			txtCourseOfferingMaxCapacity.Text = string.Empty;
			txtCourseOfferingRoomLocation.Text = string.Empty;
        }

        private void btnCourseEnrollmentSubmit_Click(object sender, RoutedEventArgs e)
        {
            bool error = false;

            //student id
            int studentID;
            txtCreateCourseEnrollmentStudentID.Background = Brushes.Transparent;
            if (!int.TryParse(txtCreateCourseEnrollmentStudentID.Text, out studentID))
            {
                txtCreateCourseEnrollmentStudentID.Background = Brushes.Red;
                error = true;
            }
            if (!StudentTable.StudentIds.Contains(studentID))
            {
                txtCreateCourseEnrollmentStudentID.Text = "Id doesnt Exist";
                txtCreateCourseEnrollmentStudentID.Background = Brushes.Red;
                error = true;
            }

            //offering id
            int offeringId;
            txtCreateCourseEnrollmentOfferingID.Background = Brushes.Transparent;
            if (!int.TryParse(txtCreateCourseEnrollmentOfferingID.Text, out offeringId))
            {
                txtCreateCourseEnrollmentOfferingID.Background = Brushes.Red;
                error = true;
            }
            if (!CourseOfferingTable.OfferingIds.Contains(offeringId))
            {
                txtCreateCourseEnrollmentOfferingID.Text = "Id Doesnt Exist";
                txtCreateCourseEnrollmentOfferingID.Background = Brushes.Red;
                error = true;
            }

            //enrollmentStatus
            txtCreateCourseEnrollmentEnrollmentStatus.Background = Brushes.Transparent;
            if (!(txtCreateCourseEnrollmentEnrollmentStatus.Text.Length > 0))
            {
                txtCreateCourseEnrollmentEnrollmentStatus.Background = Brushes.Red;
                error = true;
            }

            //finalGrade
            float finalGrade;
            txtCreateCourseEnrollmentFinalGrade.Background = Brushes.Transparent;
            if (!float.TryParse(txtCreateCourseEnrollmentFinalGrade.Text, out finalGrade))
            {
                txtCreateCourseEnrollmentFinalGrade.Background = Brushes.Red;
                error = true;
            }
			if(finalGrade < 0f || finalGrade > 100f)
			{
				txtCreateCourseEnrollmentFinalGrade.Text = "Must be between 0 and 100";
                txtCreateCourseEnrollmentFinalGrade.Background = Brushes.Red;
                error = true;
            }

            if (error)
            {
                return;
            }

            DataRow dr = db.CourseEnrollmentTable.NewRow();

            dr[0] = studentID;
            dr[1] = offeringId;
            dr[2] = txtCreateCourseEnrollmentEnrollmentStatus.Text;
            dr[3] = finalGrade;

            db.CourseEnrollmentTable.Rows.Add(dr);

            txtCreateCourseEnrollmentStudentID.Text = string.Empty;
			txtCreateCourseEnrollmentOfferingID.Text = string.Empty;
			txtCreateCourseEnrollmentEnrollmentStatus.Text = string.Empty;
			txtCreateCourseEnrollmentFinalGrade.Text = string.Empty;

            return;
        }
        private void btnCreateInstructorAssignmentSubmit_Click(object sender, RoutedEventArgs e)
        {
            bool error = false;

            //instructor id
            int instructorId;
            txtCreateInstructorAssignmentInstructorID.Background = Brushes.Transparent;
            if (!int.TryParse(txtCreateInstructorAssignmentInstructorID.Text, out instructorId))
            {
                txtCreateInstructorAssignmentInstructorID.Background = Brushes.Red;
                error = true;
            }
            if (!InstructorTable.InstructorIds.Contains(instructorId))
            {
                txtCreateInstructorAssignmentInstructorID.Text = "Id doesnt exist";
                txtCreateInstructorAssignmentInstructorID.Background = Brushes.Red;
                error = true;
            }
            //offering id
            int offeringId;
            txtCreateInstructorAssignmentOfferingID.Background = Brushes.Transparent;
            if (!int.TryParse(txtCreateInstructorAssignmentOfferingID.Text, out offeringId))
            {
                txtCreateInstructorAssignmentOfferingID.Background = Brushes.Red;
                error = true;
            }
            if (!CourseOfferingTable.OfferingIds.Contains(offeringId))
            {
                txtCreateInstructorAssignmentOfferingID.Text = "Id doesnt exist";
                txtCreateInstructorAssignmentOfferingID.Background = Brushes.Red;
                error = true;
            }

            if (error)
            {
                return;
            }

            DataRow dr = db.InstructorAssignmentTable.NewRow();
            dr[0] = instructorId;
            dr[1] = offeringId;

            db.InstructorAssignmentTable.Rows.Add(dr);

            txtCreateInstructorAssignmentInstructorID.Text = string.Empty;
            txtCreateInstructorAssignmentOfferingID.Text = string.Empty;
        }
    }
}