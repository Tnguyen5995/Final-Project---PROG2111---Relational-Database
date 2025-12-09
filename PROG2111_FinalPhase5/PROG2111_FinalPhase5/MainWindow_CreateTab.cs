using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace PROG2111_FinalPhase5
{
	/*
	 * FILE : MainWindow_CreateTab.cs
	 * PROJECT : $safeprojectname$
	 * PROGRAMMER : George Shapka
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
			}
		}
		private void btnCreateStudentSubmit_Click(object sender, RoutedEventArgs e)
		{
			bool error = false;

			//student id
			int studentID;
			txtCreateStudentID.Background = Brushes.Transparent;
			if (!int.TryParse(txtCreateStudentID.Text, out studentID))
			{
				txtCreateStudentID.Background = Brushes.Red;
				error = true;
			}
			if(StudentTable.StudentIds.Contains(studentID))
			{
				txtCreateStudentID.Text = "Id Already Exists";
				txtCreateStudentID.Background = Brushes.Red;
				error = true;
			}

			//program id
			int StudentProgramId;
			txtCreateStudentProgramID.Background = Brushes.Transparent;
			if (!int.TryParse(txtCreateStudentProgramID.Text, out StudentProgramId))
			{
				txtCreateStudentProgramID.Background = Brushes.Red;
				error = true;
			}
			if(!ProgramTable.ProgramIds.Contains(StudentProgramId))
			{
				txtCreateStudentProgramID.Text = "Program Id Doesnt Exist";
				txtCreateStudentProgramID.Background = Brushes.Red;
				error = true;
			}

			//firstname
			txtCreateStudentFirstName.Background = Brushes.Transparent;
			if(!(txtCreateStudentFirstName.Text.Length > 0))
			{
				txtCreateStudentFirstName.Background = Brushes.Red;
				error = true;
			}

			//lastname
			txtCreateStudentLastName.Background = Brushes.Transparent;
			if(!(txtCreateStudentLastName.Text.Length > 0))
			{
				txtCreateStudentLastName.Background = Brushes.Red;
				error = true;
			}

			//email
			txtCreateStudentEmail.Background = Brushes.Transparent;
			if(!(txtCreateStudentEmail.Text.Length > 0))
			{
				txtCreateStudentEmail.Background = Brushes.Red;
				error = true;
			}

			//dob
			DateTime dateOfBirth;
			dateCreateStudentDateOfBirth.Foreground = Brushes.Black;
			if(!DateTime.TryParse(dateCreateStudentDateOfBirth.Text, out dateOfBirth))
			{
				dateCreateStudentDateOfBirth.Foreground = Brushes.Red;
				error = true;
			}

			//date onrolled
			DateTime dateEnrolled;
			dateCreateStudentDateEnrolled.Foreground = Brushes.Black;
			if (!DateTime.TryParse(dateCreateStudentDateEnrolled.Text, out dateEnrolled))
			{
				dateCreateStudentDateEnrolled.Foreground = Brushes.Red;
				error = true;
			}
			if(error)
			{
				return;
			}

			StudentTable.StudentIds.Add(studentID);
			DataRow dr = db.studentTable.NewRow();

			dr[0] = studentID;
			dr[1] = StudentProgramId;
			dr[2] = txtCreateStudentFirstName.Text;
			dr[3] = txtCreateStudentLastName.Text;
			dr[4] = txtCreateStudentEmail.Text;
			dr[5] = dateOfBirth;
			dr[6] = dateEnrolled;

			db.studentTable.Rows.Add(dr);

			txtCreateStudentID.Text = string.Empty;
			txtCreateStudentProgramID.Text = string.Empty;
			txtCreateStudentFirstName.Text = string.Empty;
			txtCreateStudentLastName.Text = string.Empty;
			txtCreateStudentEmail.Text = string.Empty;
			dateCreateStudentDateOfBirth.Text = string.Empty;
			dateCreateStudentDateEnrolled.Text = string.Empty;
			return;
		}
		private void btnCreateProgramSubmit_Click(object sender, RoutedEventArgs e)
		{
			bool error = false;

			//id
			int programId;
			txtCreateProgramID.Background = Brushes.Transparent;
			if (!int.TryParse(txtCreateProgramID.Text, out programId))
			{
				txtCreateProgramID.Background = Brushes.Red;
				error = true;
			}
			if(ProgramTable.ProgramIds.Contains(programId))
			{
				txtCreateProgramID.Text = "Id already exists";
				txtCreateProgramID.Background = Brushes.Red;
				error = true;
			}

			//name
			txtCreateProgramName.Background = Brushes.Transparent;
			if(!(txtCreateProgramName.Text.Length > 0))
			{
				txtCreateProgramName.Background = Brushes.Red;
				error = true;
			}

			//credential
			txtCreateProgramCredential.Background = Brushes.Transparent;
			if(!(txtCreateProgramCredential.Text.Length > 0))
			{
				txtCreateProgramCredential.Background= Brushes.Red;
				error = true;
			}

			//duration
			int duration;
			txtCreateProgramDuration.Background = Brushes.Transparent;
			if(!int.TryParse(txtCreateProgramDuration.Text, out duration))
			{
				txtCreateProgramDuration.Background= Brushes.Red;
				error = true;
			}

			//avalible
			bool? avalible = chkCreateProgramAvaliblility.IsChecked;
			if(avalible == null)
			{
				error = true;
			}

			if(error)
			{
				return;
			}

			ProgramTable.ProgramIds.Add(programId);
			DataRow dr = db.programTable.NewRow();

			dr[0] = programId;
			dr[1] = txtCreateProgramName.Text;
			dr[2] = txtCreateProgramCredential.Text;
			dr[3] = duration;
			dr[4] = avalible;

			db.programTable.Rows.Add(dr);

			txtCreateProgramID.Text = string.Empty;
			txtCreateProgramName.Text = string.Empty;
			txtCreateProgramCredential.Text = string.Empty;
			txtCreateProgramDuration.Text = string.Empty;
			return;
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
				txtCreateProgramCourseProgramID.Text = "Id doesnt exists";
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
				txtCreateProgramCourseCourseID.Text = "Id doesnt exists";
				txtCreateProgramCourseCourseID.Background = Brushes.Red;
				error = true;
			}

			if(error)
			{
				return;
			}

			DataRow dr = db.programCourseTable.NewRow();
			dr[0] = programId;
			dr[1] = courseId;

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
			if (!ProgramTable.ProgramIds.Contains(instructorId))
			{
				txtCreateInstructorID.Text = "Id doesnt exists";
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

			txtCreateInstructorID.Text = string.Empty;
			txtCreateInstructorFirstName.Text = string.Empty;
			txtCreateInstructorLastName.Text = string.Empty;
			txtCreateInstructorEmail.Text = string.Empty;
			dateCreateInstructorHireDate.Text = string.Empty;
			txtCreateInstructorOfficeLocation.Text = string.Empty;
		}
	}
}