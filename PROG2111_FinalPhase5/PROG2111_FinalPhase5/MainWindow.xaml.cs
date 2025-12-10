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

            readProgramDataGrid.ItemsSource = db.programTable.DefaultView;
            readStudentDataGrid.ItemsSource = db.studentTable.DefaultView;

            // other read grids for courseTable, programCourseTable, etc.
            readCourseDataGrid.ItemsSource = db.courseTable.DefaultView;
            readProgramCourseDataGrid.ItemsSource = db.programCourseTable.DefaultView;
            readInstructorDataGrid.ItemsSource = db.instructorTable.DefaultView;
            readCourseOfferingDataGrid.ItemsSource = db.CourseOfferingTable.DefaultView;
            readCourseEnrollmentDataGrid.ItemsSource = db.CourseEnrollmentTable.DefaultView;
        }

        /// <summary>
        /// makes all table editors invisible
        /// </summary>
        private void SetAllTablesInvisible()
        {
            createStudentGrid.Visibility = Visibility.Hidden;
            createProgramGrid.Visibility = Visibility.Hidden;
            createCourseGrid.Visibility = Visibility.Hidden;
            createProgramCourseGrid.Visibility = Visibility.Hidden;
            createInstructorGrid.Visibility = Visibility.Hidden;
            createCourseOfferingGrid.Visibility = Visibility.Hidden;
            createCourseEnrollmentGrid.Visibility = Visibility.Hidden;
            createInstructorAssignmentGrid.Visibility = Visibility.Hidden;

            readStudentDataGrid.Visibility = Visibility.Hidden;
            readProgramDataGrid.Visibility = Visibility.Hidden;
            readCourseDataGrid.Visibility = Visibility.Hidden;
            readProgramCourseDataGrid.Visibility = Visibility.Hidden;
            readInstructorDataGrid.Visibility = Visibility.Hidden;
            readCourseOfferingDataGrid.Visibility = Visibility.Hidden;
            readCourseEnrollmentDataGrid.Visibility = Visibility.Hidden;
            readInstructorAssignmentDataGrid.Visibility = Visibility.Hidden;
            readStudentDataGrid.Visibility = Visibility.Hidden;
            readProgramDataGrid.Visibility = Visibility.Hidden;
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

            ProgramCourseTable.ProgramCourseKeys.Add(new int[] { 1, 2 });
            ProgramCourseTable.ProgramCourseKeys.Add(new int[] { 2, 1 });
            ProgramCourseTable.ProgramCourseKeys.Add(new int[] { 3, 3 });

            //add instructors (instructor id, fName, lName, email, hire date, office location)
            db.instructorTable.Rows.Add(1, "billy", "bob", "billy@mail.com", new DateTime(2020, 6, 8), "Waterloo");
            db.instructorTable.Rows.Add(2, "john", "johnson", "jj@mail.com", new DateTime(2018, 8, 1), "cambridge");

            InstructorTable.InstructorIds.Add(1);
            InstructorTable.InstructorIds.Add(2);

            //add course offering (offering id, course id, term start, term end, acedemic year, schedule info, selection code, delivery mode, max capacity, room location)
            db.CourseOfferingTable.Rows.Add(1, 2, 1, 1, 2025, "idk", 3, "in person", 30, "12b14");
            db.CourseOfferingTable.Rows.Add(2, 3, 2, 3, 2024, "idk", 1, "online", 50, "home");

            CourseOfferingTable.OfferingIds.Add(1);
            CourseOfferingTable.OfferingIds.Add(2);

            //add courseEnrollment (student id, offering id, enrollmentStatus, finalGrade)
            db.CourseEnrollmentTable.Rows.Add(1, 2, "enrolled", 0f);
            db.CourseEnrollmentTable.Rows.Add(2, 1, "complete", 80.33f);

            //add instructor assignment (instructor Id, offering Id)
            db.InstructorAssignmentTable.Rows.Add(2, 2);
            db.InstructorAssignmentTable.Rows.Add(1, 3);
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

        private void btnCreateStudentSubmit_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Add logic to handle student creation here
            MessageBox.Show("Student Submit button clicked!");
        }

        private void btnCreateProgramSubmit_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Add logic to handle the Program Submit button click
            MessageBox.Show("Program Submit button clicked!");
        }

        private void btnCreateInstructorAssignmentSubmit_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Add logic to handle instructor assignment submission
            MessageBox.Show("Instructor Assignment Submitted!");
        }
    }
}