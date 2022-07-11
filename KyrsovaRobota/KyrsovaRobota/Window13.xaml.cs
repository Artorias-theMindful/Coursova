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
    /// Логика взаимодействия для Window13.xaml
    /// </summary>
    public partial class Window13 : Window
    {
        SqlConnection connection = null;
        string connectionString = null;
        SqlCommand command;
        SqlDataAdapter adapter;
        public Window13()
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
            connection.Open();
            command = new SqlCommand("select SubjectName from Предмети", connection);

            sqlDataReader = command.ExecuteReader();
            while (sqlDataReader.Read())
            {

                Cb3.Items.Add(sqlDataReader.GetValue(0));

            }
            connection.Close();
            Cb3.SelectedIndex = -1;
        }
        private void Cb1_DropDownClosed(object sender, EventArgs e)
        {
            Lb1.Content = "";
            Cb2.Items.Clear();
            Cb2.SelectedIndex = -1;
            Cb3.IsEnabled = true;
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

            command = new SqlCommand("select SubjectName, Date, Auditory from Консультації inner join Кафедри on Кафедри.IDCathedra = Консультації.IDCathedra inner join Групи on Групи.IDCathedra = Кафедри.IDCathedra inner join Абітуріенти on Абітуріенти.GroupID = Групи.IDGroup inner join Предмети on Предмети.IDsubject = Консультації.IDSubject where Surname = '" + Cb2.Text + "' and SubjectName = '" + Cb3.Text + "'", connection);
            SqlDataReader sqlDataReader = command.ExecuteReader();
            string consultation = "";
            while (sqlDataReader.Read())
            {

                consultation += sqlDataReader.GetValue(0).ToString() + " " + sqlDataReader.GetValue(1).ToString() + " " + sqlDataReader.GetValue(2).ToString();

            }

            connection.Close();
            connection.Open();

            command = new SqlCommand("select SubjectName, Date, Auditory from Екзамени inner join Групи on Групи.IDGroup = Екзамени.IDGroup inner join Абітуріенти on Абітуріенти.GroupID = Групи.IDGroup inner join Предмети on Предмети.IDsubject = Екзамени.IDSubject where Surname = '" + Cb2.Text + "' and SubjectName = '" + Cb3.Text + "'", connection);
            sqlDataReader = command.ExecuteReader();
            string exams = "";
            while (sqlDataReader.Read())
            {

                exams += sqlDataReader.GetValue(0).ToString() + " " + sqlDataReader.GetValue(1).ToString() + " " + sqlDataReader.GetValue(2).ToString();

            }
            Lb1.Content = "Консультація - " + consultation + "\n Екзамен - " + exams;
            connection.Close();
        }

        private void Cb3_DropDownClosed(object sender, EventArgs e)
        {
            Cb2.IsEnabled = true;
            Cb2_DropDownClosed(sender, e);
        }
    }
}
