using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PROG2111_FinalPhase5
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static readonly string ComboBoxString = "System.Windows.Controls.ComboBoxItem: ";
        Database db = new Database();
        public MainWindow()
        {
            InitializeComponent();
            readStudentDataGrid.ItemsSource = db.studentTable.DefaultView;
            readProgramDataGrid.ItemsSource = db.programTable.DefaultView;
        }

        /// <summary>
        /// makes all table editors invisable
        /// </summary>
        private void SetAllTablesInvisible()
        {
            createStudentGrid.Visibility = Visibility.Hidden;
            createProgramGrid.Visibility = Visibility.Hidden;
            readStudentDataGrid.Visibility = Visibility.Hidden;
            readProgramDataGrid.Visibility = Visibility.Hidden;
        }

        private void btnFillTables_Click(object sender, RoutedEventArgs e)
        {
            //add programs (id, name, credential, duration, avalible)
            db.programTable.Rows.Add(1, "math", "idk", 3, true);
            db.programTable.Rows.Add(2, "c++", "idk", 2, false);
            db.programTable.Rows.Add(3, "writing", "idk", 2, true);

            //add students (id, program id, fName, lName, email, dob, enrollment date)
            db.studentTable.Rows.Add(1, 1, "Fred", "Smith", "Fred@mail.ca", new DateTime(2001, 5, 12), new DateTime(2025, 9, 1));
            db.studentTable.Rows.Add(2, 3, "john", "apple", "applej@mail.ca", new DateTime(1991, 4, 22), new DateTime(2025, 9, 1));
        }
    }
}
