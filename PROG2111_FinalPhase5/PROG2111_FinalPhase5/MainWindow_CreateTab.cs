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
            string selection = createComboBox.SelectedItem.ToString();
            selection = selection.Remove(0, ComboBoxString.Length);
            //MessageBox.Show(selection);

            switch (selection)
            {
                case "Student Table":
                    createStudentGrid.Visibility = Visibility.Visible;
                    break;
            }
        }
        private void btnStudentSubmit_Click(object sender, RoutedEventArgs e)
        {
            bool error = false;

            //student id
            int studentID;
            txtCreateStudentID.Foreground = Brushes.Black;
            if (!int.TryParse(txtCreateStudentID.Text, out studentID))
            {
                txtCreateStudentID.Foreground = Brushes.Red;
                error = true;
            }
            if(StudentTable.StudentIds.Contains(studentID))
            {
                txtCreateStudentID.Text = "Id Already Exists";
                txtCreateStudentID.Foreground = Brushes.Red;
                error = true;
            }

            //program id
            int StudentProgramId;
            txtCreateStudentProgramID.Foreground = Brushes.Black;
            if (!int.TryParse(txtCreateStudentProgramID.Text, out StudentProgramId))
            {
                txtCreateStudentProgramID.Foreground = Brushes.Red;
                error = true;
            }
            if(StudentTable.StudentProgramIds.Contains(StudentProgramId))
            {
                txtCreateStudentProgramID.Text = "Id Already Exists";
                txtCreateStudentProgramID.Foreground = Brushes.Red;
                error = true;
            }

            //firstname
            txtCreateStudentFirstName.Foreground = Brushes.Black;
            if(!(txtCreateStudentFirstName.Text.Length > 0))
            {
                txtCreateStudentFirstName.Foreground = Brushes.Red;
                error = true;
            }

            //lastname
            txtCreateStudentLastName.Foreground = Brushes.Black;
            if(!(txtCreateStudentFirstName.Text.Length > 0))
            {
                txtCreateStudentLastName.Foreground = Brushes.Red;
                error = true;
            }

            //email
            txtCreateStudentEmail.Foreground = Brushes.Black;
            if(!(txtCreateStudentFirstName.Text.Length > 0))
            {
                txtCreateStudentEmail.Foreground = Brushes.Red;
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
            StudentTable.StudentProgramIds.Add(StudentProgramId);
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
    }
}