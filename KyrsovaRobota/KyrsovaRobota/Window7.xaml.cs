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
    public partial class Window7 : Window
    {
        SqlConnection connection = null;
        string connectionString = null;
        SqlCommand command;
        SqlDataAdapter adapter;
        public Window7()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["DefConnection"].ConnectionString;
            connection = new SqlConnection(connectionString);
            ShowData("SELECT dbo.Консультації.Date, dbo.Кафедри.CathedraName, dbo.Предмети.SubjectName FROM     dbo.Консультації INNER JOIN dbo.Кафедри ON dbo.Консультації.IDCathedra = dbo.Кафедри.IDCathedra INNER JOIN dbo.Предмети ON dbo.Консультації.IDSubject = dbo.Предмети.IDsubject", dataGrid);

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
            command = new SqlCommand("select CathedraName from Кафедри", connection);
            sqlDataReader = command.ExecuteReader();

            while (sqlDataReader.Read())
            {

                Cb1.Items.Add(sqlDataReader.GetValue(0));

            }
            connection.Close();
        }
        private void Cb1_DropDownClosed(object sender, EventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {


            try
            {
                connection.Open();
                command = new SqlCommand("select IDCathedra from Кафедри where CathedraName = '" + Cb1.Text + "'", connection);
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



                command = new SqlCommand("select Auditory from Консультації where IDSubject =" + id1.ToString() + "and IDCathedra = " + id.ToString(), connection);
                int d = 0;
                sqlDataReader = command.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    d++;
                }
                if(d > 0)
                {
                    MessageBox.Show("Консультація для заданої кафедри на заданий предмет вже існує");
                    connection.Close();
                    return;
                }
                
                connection.Close();
                connection.Open();
                command = new SqlCommand("insert into Консультації (IDCathedra, Date, Auditory, IDSubject) values (" + id.ToString() + ", '" + +Data.SelectedDate.Value.Year + "-" + Data.SelectedDate.Value.Month + "-" + Data.SelectedDate.Value.Day + "', " + Tb1.Text + ", " + id1.ToString() + ")", connection);
                command.ExecuteNonQuery();
                connection.Close();
                ShowData("SELECT dbo.Консультації.Date, dbo.Кафедри.CathedraName, dbo.Предмети.SubjectName FROM     dbo.Консультації INNER JOIN dbo.Кафедри ON dbo.Консультації.IDCathedra = dbo.Кафедри.IDCathedra INNER JOIN dbo.Предмети ON dbo.Консультації.IDSubject = dbo.Предмети.IDsubject", dataGrid);
                MessageBox.Show("Консультація запланована");
            }
            catch(Exception h)
            {
                connection.Close();
                MessageBox.Show(h.Message);
            }
        }
    }
}
