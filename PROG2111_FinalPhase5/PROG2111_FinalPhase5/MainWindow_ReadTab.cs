using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace PROG2111_FinalPhase5
{
    /*
	 * FILE : MainWindow_ReadTab.cs
	 * PROJECT : $safeprojectname$
	 * PROGRAMMER : George Shapka
	 * FIRST VERSION : 12/8/2025 5:35:53 PM
	 */
    public partial class MainWindow : Window
    {
        private void readComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SetAllTablesInvisible();
            string selection = readComboBox.SelectedItem.ToString();
            selection = selection.Remove(0, ComboBoxString.Length);

            switch (selection)
            {
                case "Student Table":
                    readStudentDataGrid.Visibility = Visibility.Visible;
                    break;
                case "Program Table":
                    readProgramDataGrid.Visibility = Visibility.Visible;
                    break;
                case "Course Table":
                    readCourseDataGrid.Visibility = Visibility.Visible;
                    break;
                case "Program Course Table":
                    readProgramCourseDataGrid.Visibility = Visibility.Visible;
                    break;
                case "Instructor Table":
                    readInstructorDataGrid.Visibility = Visibility.Visible;
                    break;
                case "Course Offering Table":
                    readCourseOfferingDataGrid.Visibility = Visibility.Visible;
                    break;
                case "Course Enrollment Table":
                    readCourseEnrollmentDataGrid.Visibility = Visibility.Visible;
                    break;
                case "Instructor Assignment Table":
                    readInstructorAssignmentDataGrid.Visibility = Visibility.Visible;
                    break;
            }
        }

        private void btnDeleteStudent_Click(object sender, RoutedEventArgs e)
        {
            if (readStudentDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Please select a student row to update.", "No Selection",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            DataRowView rowView = readStudentDataGrid.SelectedItem as DataRowView;
            if (rowView == null)
            {
                return;
            }

            int studentId = Convert.ToInt32(rowView["studentId"]); // matches StudentTable column

            if (MessageBox.Show($"Delete student {studentId} and their enrollments?",
                                "Confirm Delete",
                                MessageBoxButton.YesNo,
                                MessageBoxImage.Warning) != MessageBoxResult.Yes)
            {
                return;
            }

            try
            {
                db.DeleteStudent(studentId);   // uses MySQL transaction
                RefreshReadGrids();
                MessageBox.Show("Student deleted.", "Success",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting student:\n" + ex.Message,
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void btnDeleteProgram_Click(object sender, RoutedEventArgs e)
        {
            if (readProgramDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Please select a program row to update.", "No Selection",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            DataRowView rowView = readProgramDataGrid.SelectedItem as DataRowView;
            if (rowView == null)
            {
                return;
            }


            int programId = Convert.ToInt32(rowView["programID"]);

            var result = MessageBox.Show($"Delete program {programId}?",
                "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result != MessageBoxResult.Yes)
            {
                return;
            }

            try
            {
                db.DeleteProgram(programId);
                RefreshReadGrids();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting program:\n" + ex.Message,
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnUpdateStudent_Click(object sender, RoutedEventArgs e)
        {
            if (readStudentDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Please select a student row to update.", "No Selection",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            DataRowView rowView = readStudentDataGrid.SelectedItem as DataRowView;
            if (rowView == null)
            {
                return;
            }


            try
            {
                StudentModel student = new StudentModel
                {
                    StudentId = Convert.ToInt32(rowView["studentId"]),
                    ProgramId = Convert.ToInt32(rowView["programId"]),
                    FirstName = rowView["firstName"].ToString(),
                    LastName = rowView["lastName"].ToString(),
                    EmailAddress = rowView["email"].ToString(),
                    DateOfBirth = rowView["dateOfBirth"] == DBNull.Value
                                   ? (DateTime?)null
                                   : (DateTime)rowView["dateOfBirth"],
                    DateEnrolled = (DateTime)rowView["dateEnrolled"]
                };

                db.UpdateStudent(student);
                RefreshReadGrids();

                MessageBox.Show("Student updated.", "Success",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating student:\n" + ex.Message,
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void btnUpdateProgram_Click(object sender, RoutedEventArgs e)
        {
            if (readProgramDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Please select a program row to update.", "No Selection",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            DataRowView rowView = readProgramDataGrid.SelectedItem as DataRowView;
            if (rowView == null)
            {
                return;
            }

            try
            {
                ProgramModel program = new ProgramModel
                {
                    ProgramId = Convert.ToInt32(rowView["programID"]),
                    ProgramName = Convert.ToString(rowView["programName"]),
                    CredentialType = Convert.ToString(rowView["credentialType"]),
                    DurationInTerms = Convert.ToByte(rowView["durationInTerms"]),
                    IsAvailable = Convert.ToBoolean(rowView["isAvailable"])
                };

                db.UpdateProgram(program);
                RefreshReadGrids();

                MessageBox.Show("Program updated.", "Success",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating program:\n" + ex.Message,
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


    }
}//end of PROG2111_FinalPhase5