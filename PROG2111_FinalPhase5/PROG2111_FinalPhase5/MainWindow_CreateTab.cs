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
    }
}