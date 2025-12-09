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
            readCourseDataGrid.ItemsSource = db.courseTable.DefaultView;
            readProgramCourseDataGrid.ItemsSource = db.programCourseTable.DefaultView;
        }

        /// <summary>
        /// makes all table editors invisable
        /// </summary>
        private void SetAllTablesInvisible()
        {
            createStudentGrid.Visibility = Visibility.Hidden;
            createProgramGrid.Visibility = Visibility.Hidden;
            createCourseGrid.Visibility = Visibility.Hidden;
            createProgramCourseGrid.Visibility = Visibility.Hidden;

            readStudentDataGrid.Visibility = Visibility.Hidden;
            readProgramDataGrid.Visibility = Visibility.Hidden;
            readCourseDataGrid.Visibility = Visibility.Hidden;
            readProgramCourseDataGrid.Visibility = Visibility.Hidden;
        }

        private void btnFillTables_Click(object sender, RoutedEventArgs e)
        {
            //disable to not allow overlapping entries
            btnFillTables.IsEnabled = false;

            //add programs (id, name, credential, duration, avalible)
            db.programTable.Rows.Add(1, "SET", "adv Diploma", 6, true);
            db.programTable.Rows.Add(2, "mEng", "degree", 8, false);
            db.programTable.Rows.Add(3, "writing", "certificate", 2, true);

            ProgramTable.ProgramIds.Add(1);
            ProgramTable.ProgramIds.Add(2);
            ProgramTable.ProgramIds.Add(3);

            //add students (id, program id, fName, lName, email, dob, enrollment date)
            db.studentTable.Rows.Add(1, 1, "Fred", "Smith", "Fred@mail.ca", new DateTime(2001, 5, 12), new DateTime(2025, 9, 1));
            db.studentTable.Rows.Add(2, 3, "john", "apple", "applej@mail.ca", new DateTime(1991, 4, 22), new DateTime(2025, 9, 1));

            StudentTable.StudentIds.Add(1);
            StudentTable.StudentIds.Add(2);

            //add course (id, title, desc, hours)
            db.courseTable.Rows.Add(1, "math", "calculashiunms", 50);
            db.courseTable.Rows.Add(2, "c++", "mcdonalds prep", 86);
            db.courseTable.Rows.Add(3, "zombies???", "goofy ahh class", 54);

            CourseTable.CourseIds.Add(1);
            CourseTable.CourseIds.Add(2);
            CourseTable.CourseIds.Add(3);

            //add program course (program id, course id)
            db.programCourseTable.Rows.Add(1, 2);
            db.programCourseTable.Rows.Add(2, 1);
            db.programCourseTable.Rows.Add(3, 3);
        }
    }
}