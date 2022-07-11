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
    /// Логика взаимодействия для Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        SqlConnection connection = null;
        string connectionString = null;
        SqlCommand command;
        SqlDataAdapter adapter;
        public Window2()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["DefConnection"].ConnectionString;
            ShowData("SELECT dbo.Абітуріенти.Surname, dbo.Предмети.SubjectName, dbo.Студенти_Оцінки.Mark FROM     dbo.Студенти_Оцінки INNER JOIN dbo.Абітуріенти ON dbo.Студенти_Оцінки.IDStudent = dbo.Абітуріенти.ExamList INNER JOIN dbo.Предмети ON dbo.Студенти_Оцінки.IDSubject = dbo.Предмети.IDsubject", dataGrid);
            connection = new SqlConnection(connectionString);
            connection.Open();

            command = new SqlCommand("select GroupName from Групи", connection);
            SqlDataReader sqlDataReader = command.ExecuteReader();

            while (sqlDataReader.Read())
            {
                Cb1.Items.Add(sqlDataReader.GetValue(0));

            }
            Cb1.SelectedItem = 1;

            connection.Close();
            connection.Open();

            command = new SqlCommand("select SubjectName from Предмети", connection);
            sqlDataReader = command.ExecuteReader();

            while (sqlDataReader.Read())
            {
                Cb2.Items.Add(sqlDataReader.GetValue(0));

            }
            Cb2.SelectedItem = 1;

            connection.Close();
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
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            connection.Open();
            command = new SqlCommand("select Mark from Студенти_Оцінки inner join Абітуріенти on Студенти_Оцінки.IDStudent = Абітуріенти.ExamList inner join Предмети on Студенти_Оцінки.IDSubject = Предмети.IDsubject where  Surname = '" + Cb3.Text + "' and SubjectName = '" + Cb2.Text + "'", connection);
            SqlDataReader sqlDataReader = command.ExecuteReader();
            
            while (sqlDataReader.Read())
            {
                try
                {
                    Convert.ToInt32(sqlDataReader.GetValue(0));
                    MessageBox.Show("Ви не можете поставити оцінку, бо вона вже там стоїть. Якщо хочете змінити оцінку, зайдіть у меню апіляції");
                    connection.Close();
                    return;
                }
                catch
                {
                    
                }
                
            }
            connection.Close();
            connection.Open();
            
            command = new SqlCommand("update Студенти_Оцінки set Mark = " + Tb1.Text + " from Студенти_Оцінки inner join Абітуріенти on Студенти_Оцінки.IDStudent = Абітуріенти.ExamList inner join Предмети on Студенти_Оцінки.IDSubject = Предмети.IDsubject where  Surname = '" + Cb3.Text + "' and SubjectName = '" + Cb2.Text + "'",connection);
            command.ExecuteNonQuery();
            
            
            connection.Close();
            ShowData("SELECT dbo.Абітуріенти.Surname, dbo.Предмети.SubjectName, dbo.Студенти_Оцінки.Mark FROM     dbo.Студенти_Оцінки INNER JOIN dbo.Абітуріенти ON dbo.Студенти_Оцінки.IDStudent = dbo.Абітуріенти.ExamList INNER JOIN dbo.Предмети ON dbo.Студенти_Оцінки.IDSubject = dbo.Предмети.IDsubject", dataGrid);
            connection.Open();
            command = new SqlCommand("select Mark  " +
                "from Студенти_Оцінки inner join Абітуріенти on ExamList = IDStudent " +
                "inner join Групи on GroupID = IDGroup " +
                "inner join Кафедри on Групи.IDCathedra = Кафедри.IDCathedra " +
                "inner join Факультети on Факультети.IDFacultative = Кафедри.IDFacultative " +
                "where Surname = '" + Cb3.Text + "' and Mark is not null", connection);
            double b = 0;
            
            sqlDataReader = command.ExecuteReader();
            while (sqlDataReader.Read())
            {
                b += Convert.ToDouble(sqlDataReader.GetValue(0));
            }
            connection.Close();
            connection.Open();
            command = new SqlCommand("select Medal  " +
                "from Студенти_Оцінки inner join Абітуріенти on ExamList = IDStudent " +
                "inner join Групи on GroupID = IDGroup " +
                "inner join Кафедри on Групи.IDCathedra = Кафедри.IDCathedra " +
                "inner join Факультети on Факультети.IDFacultative = Кафедри.IDFacultative " +
                "where Surname = '" + Cb3.Text + "'", connection);
            sqlDataReader = command.ExecuteReader();
            string s = "";
            while (sqlDataReader.Read())
            {
                s += sqlDataReader.GetValue(0).ToString();
            }
            connection.Close();
            if (s == "Золота" || s == "Срібна")
            {
                connection.Open();
                command = new SqlCommand("update Абітуріенти set AvScore = " + b.ToString() + " where Surname = '" + Cb3.Text + "'", connection);
                command.ExecuteReader();
                connection.Close();
            }
            else
            {
                connection.Open();
                command = new SqlCommand("update Абітуріенти set AvScore = " + (b/4).ToString().Replace(',', '.') + " where Surname = '" + Cb3.Text + "'", connection);
                command.ExecuteNonQuery();
                connection.Close();
            }
            
            Cb1.SelectedIndex = -1;
            Cb2.SelectedIndex = -1;
            Cb3.SelectedIndex = -1;
            Tb1.Text = "";
        }

        private void Cb1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ShowData("SELECT dbo.Абітуріенти.Surname, dbo.Предмети.SubjectName, dbo.Студенти_Оцінки.Mark FROM     dbo.Студенти_Оцінки INNER JOIN dbo.Абітуріенти ON dbo.Студенти_Оцінки.IDStudent = dbo.Абітуріенти.ExamList INNER JOIN dbo.Предмети ON dbo.Студенти_Оцінки.IDSubject = dbo.Предмети.IDsubject inner join Групи on Абітуріенти.GroupID = Групи.IDGroup where GroupName = '" + ((sender as ComboBox).SelectedItem as string) + "'", dataGrid);
            Cb3.Items.Clear();
            Cb3.IsEnabled = true;
            connection.Open();
            command = new SqlCommand("select Surname from Студенти_Оцінки inner join Абітуріенти on Студенти_Оцінки.IDStudent = Абітуріенти.ExamList inner join Групи on Абітуріенти.GroupID = Групи.IDGroup where  Mark is null and GroupName = '" + ((sender as ComboBox).SelectedItem as string) + "'", connection);
            SqlDataReader sqlDataReader = command.ExecuteReader();
            while (sqlDataReader.Read())
            {
                if (!Cb3.Items.Contains(sqlDataReader.GetValue(0)))
                    Cb3.Items.Add(sqlDataReader.GetValue(0));
            }
            
            connection.Close();
        }
    }
}
