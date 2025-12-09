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
        private readonly Database db = new Database();

        public MainWindow()
        {
            InitializeComponent();
            RefreshReadGrids();
        }

        /// <summary>
        /// Reloads data from the database and binds to the read grids.
        /// </summary>
        private void RefreshReadGrids()
        {
            db.RefreshProgramTable();
            db.RefreshStudentTable();

            readStudentDataGrid.ItemsSource = db.studentTable.DefaultView;
            readProgramDataGrid.ItemsSource = db.programTable.DefaultView;
        }

        /// <summary>
        /// makes all table editors invisible
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
            try
            {
                // Example seed programs
                ProgramModel program1 = new ProgramModel
                {
                    ProgramId = 1,
                    ProgramName = "Math",
                    CredentialType = "Diploma",
                    DurationInTerms = 3,
                    IsAvailable = true
                };

                ProgramModel program2 = new ProgramModel
                {
                    ProgramId = 2,
                    ProgramName = "C++ Programming",
                    CredentialType = "Certificate",
                    DurationInTerms = 2,
                    IsAvailable = false
                };

                db.InsertProgram(program1);
                db.InsertProgram(program2);

                // Example seed students
                StudentModel student1 = new StudentModel
                {
                    StudentId = 1,
                    ProgramId = 1,
                    FirstName = "Fred",
                    LastName = "Smith",
                    EmailAddress = "fred@mail.ca",
                    DateOfBirth = new DateTime(2001, 5, 12),
                    DateEnrolled = new DateTime(2025, 9, 1)
                };

                StudentModel student2 = new StudentModel
                {
                    StudentId = 2,
                    ProgramId = 2,
                    FirstName = "John",
                    LastName = "Apple",
                    EmailAddress = "applej@mail.ca",
                    DateOfBirth = new DateTime(1991, 4, 22),
                    DateEnrolled = new DateTime(2025, 9, 1)
                };

                db.InsertStudent(student1);
                db.InsertStudent(student2);

                RefreshReadGrids();

                MessageBox.Show("Sample data inserted into CourseRegProDB.", "Seed Data",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inserting sample data:\n" + ex.Message, "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}