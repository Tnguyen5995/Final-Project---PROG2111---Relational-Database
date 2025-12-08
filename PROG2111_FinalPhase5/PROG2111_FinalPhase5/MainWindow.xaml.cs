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
        }

        /// <summary>
        /// makes all table editors invisable
        /// </summary>
        private void SetAllTablesInvisible()
        {
            createStudentGrid.Visibility = Visibility.Hidden;
        }
    }
}
