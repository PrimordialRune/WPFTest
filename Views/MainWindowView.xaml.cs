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

namespace Games.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindowView : Window
    {
        public MainWindowView()
        {
            InitializeComponent();

            /*try
            {
                SqlConnection thisConnection = new SqlConnection(@"Data Source=TIQ-STAGE;Initial Catalog=games;Integrated Security=True");
                thisConnection.Open();

                string Get_Data = "SELECT * FROM emp";

                SqlCommand cmd = thisConnection.CreateCommand();
                cmd.CommandText = Get_Data;

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("emp");
                sda.Fill(dt);

                dataGrid1.ItemsSource = dt.DefaultView;
            }
            catch
            {
                MessageBox.Show("db error");
            }*/
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void _btnAdd_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
