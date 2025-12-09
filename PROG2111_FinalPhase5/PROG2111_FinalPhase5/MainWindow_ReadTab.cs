using System;
using System.Collections.Generic;
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
            }
        }

    }
}//end of PROG2111_FinalPhase5