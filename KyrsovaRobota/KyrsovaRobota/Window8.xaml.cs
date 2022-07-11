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
    /// Логика взаимодействия для Window7.xaml
    /// </summary>
    public partial class Window8 : Window
    {
        SqlConnection connection = null;
        string connectionString = null;
        SqlCommand command;
        SqlDataAdapter adapter;
        public Window8()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["DefConnection"].ConnectionString;
            connection = new SqlConnection(connectionString);
            ShowData("SELECT dbo.Екзамени.Date, dbo.Групи.GroupName, dbo.Предмети.SubjectName FROM     dbo.Екзамени INNER JOIN dbo.Групи ON dbo.Екзамени.IDGroup = dbo.Групи.IDGroup INNER JOIN dbo.Предмети ON dbo.Екзамени.IDSubject = dbo.Предмети.IDsubject", dataGrid);

            Update();
        }

        void ShowData(string SqlQuery, DataGrid dataGrid)
        {
            connection = new SqlConnection(connectionString);

            connection.Open();

            command = new SqlCommand(SqlQuery, connection);
            adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            dataGrid.ItemsSource = table.DefaultView;

            connection.Close();

        }
        void Update()
        {


            connection.Open();

            command = new SqlCommand("select SubjectName from Предмети", connection);

            SqlDataReader sqlDataReader = command.ExecuteReader();
            while (sqlDataReader.Read())
            {

                Cb2.Items.Add(sqlDataReader.GetValue(0));

            }
            connection.Close();
            connection.Open();
            command = new SqlCommand("select GroupName from Групи", connection);
            sqlDataReader = command.ExecuteReader();

            while (sqlDataReader.Read())
            {

                Cb1.Items.Add(sqlDataReader.GetValue(0));

            }
            connection.Close();
        }
       

        private void Button_Click(object sender, RoutedEventArgs e)
        {


            try
            {
                connection.Open();
                command = new SqlCommand("select IDGroup from Групи where GroupName = '" + Cb1.Text + "'", connection);
                SqlDataReader sqlDataReader = command.ExecuteReader();
                int id = 0;
                while (sqlDataReader.Read())
                {
                    id = Convert.ToInt32(sqlDataReader.GetValue(0));
                }
                connection.Close();
                connection.Open();

                command = new SqlCommand("select IDSubject from Предмети where SubjectName = '" + Cb2.Text + "'", connection);
                sqlDataReader = command.ExecuteReader();
                int id1 = 0;
                while (sqlDataReader.Read())
                {
                    id1 = Convert.ToInt32(sqlDataReader.GetValue(0));
                }
                connection.Close();
                connection.Open();



                command = new SqlCommand("select Auditory from Екзамени where IDSubject =" + id1.ToString() + "and IDGroup = " + id.ToString(), connection);
                int d = 0;
                sqlDataReader = command.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    d++;
                }
                if (d > 0)
                {
                    MessageBox.Show("Екзамен для заданої групи на заданий предмет вже існує");
                    connection.Close();
                    return;
                }

                connection.Close();
                connection.Open();
                command = new SqlCommand("insert into Екзамени (IDGroup, Date, Auditory, IDSubject) values (" + id.ToString() + ", '" + +Data.SelectedDate.Value.Year + "-" + Data.SelectedDate.Value.Month + "-" + Data.SelectedDate.Value.Day + "', " + Tb1.Text + ", " + id1.ToString() + ")", connection);
                command.ExecuteNonQuery();
                connection.Close();
                ShowData("SELECT dbo.Екзамени.Date, dbo.Групи.GroupName, dbo.Предмети.SubjectName FROM     dbo.Екзамени INNER JOIN dbo.Групи ON dbo.Екзамени.IDGroup = dbo.Групи.IDGroup INNER JOIN dbo.Предмети ON dbo.Екзамени.IDSubject = dbo.Предмети.IDsubject", dataGrid);
                MessageBox.Show("Екзамен запланований");
            }
            catch (Exception h)
            {
                MessageBox.Show(h.Message);
            }
        }
    }
}
