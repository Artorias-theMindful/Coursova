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
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace KyrsovaRobota
{
    /// <summary>
    /// Логика взаимодействия для Window12.xaml
    /// </summary>
    public partial class Window12 : Window
    {
        SqlConnection connection = null;
        string connectionString = null;
        SqlCommand command;
        SqlDataAdapter adapter;
        public Window12()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["DefConnection"].ConnectionString;
            connection = new SqlConnection(connectionString);
            connection.Open();
            command = new SqlCommand("select GroupName from Групи", connection);

            SqlDataReader sqlDataReader = command.ExecuteReader();
            while (sqlDataReader.Read())
            {

                Cb1.Items.Add(sqlDataReader.GetValue(0));

            }
            connection.Close();
        }

        private void Cb1_DropDownClosed(object sender, EventArgs e)
        {
            Cb2.Items.Clear();
            Cb2.SelectedIndex = -1;
            dataGrid.ItemsSource = null;
            Cb2.IsEnabled = true;
            connection.Open();
            command = new SqlCommand("select Surname from Абітуріенти inner join Групи on Групи.IDGroup = Абітуріенти.GroupID where GroupName = '" + Cb1.Text + "'", connection);

            SqlDataReader sqlDataReader = command.ExecuteReader();
            while (sqlDataReader.Read())
            {

                Cb2.Items.Add(sqlDataReader.GetValue(0));

            }
            connection.Close();
        }

        private void Cb2_DropDownClosed(object sender, EventArgs e)
        {
            

            connection.Open();

            command = new SqlCommand("select SubjectName, Mark from Студенти_Оцінки inner join Абітуріенти on Абітуріенти.ExamList = IDStudent inner join Предмети on Предмети.IDsubject = Студенти_Оцінки.IDSubject where Surname = '" + Cb2.Text  + "'", connection);
            adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            dataGrid.ItemsSource = table.DefaultView;

            connection.Close();
        }
    }
}
