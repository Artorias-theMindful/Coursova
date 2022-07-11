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
    /// Логика взаимодействия для Window10.xaml
    /// </summary>
    public partial class Window10 : Window
    {
        SqlConnection connection = null;
        string connectionString = null;
        SqlCommand command;
        SqlDataAdapter adapter;
        static int amount = 0;
        public Window10()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["DefConnection"].ConnectionString;
            connection = new SqlConnection(connectionString);
            connection.Open();

            command = new SqlCommand("select FacultativeName from Факультети", connection);

            SqlDataReader sqlDataReader = command.ExecuteReader();
            while (sqlDataReader.Read())
            {

                Cb1.Items.Add(sqlDataReader.GetValue(0));

            }
            connection.Close();
            connection.Open();

            command = new SqlCommand("select CathedraName from Кафедри", connection);

            sqlDataReader = command.ExecuteReader();
            while (sqlDataReader.Read())
            {

                Cb2.Items.Add(sqlDataReader.GetValue(0));

            }
            connection.Close();
            connection.Open();

            command = new SqlCommand("select AmountOfPlaces, FacultativeName from Факультети", connection);
            sqlDataReader = command.ExecuteReader();
            
            List<int> places = new List<int>();
            List<string> facultatives = new List<string>();
            while (sqlDataReader.Read())
            {
                places.Add(Convert.ToInt32(sqlDataReader.GetValue(0)));
               
                facultatives.Add(sqlDataReader.GetValue(1).ToString());
            }
            connection.Close();
            List<string> students = new List<string>();
            double avr = 0;
            for (int i = 0; i < facultatives.Count; i++)
            {
                connection.Open();

                command = new SqlCommand("select top " + places[i].ToString() + " Surname, AvScore from Абітуріенти inner join Групи on Групи.IDGroup = Абітуріенти.GroupID inner join Кафедри on Кафедри.IDCathedra = Групи.IDCathedra inner join Факультети on Факультети.IDFacultative = Кафедри.IDFacultative where FacultativeName = '" + facultatives[i] + "' order by AvScore desc", connection);
                sqlDataReader = command.ExecuteReader();
                
                while (sqlDataReader.Read())
                {

                    students.Add(sqlDataReader.GetValue(0).ToString());
                    avr = Convert.ToDouble(sqlDataReader.GetValue(1));
                }
                connection.Close();
            }
            Lb3.Content += avr.ToString();
            Lb4.Content += students.Count.ToString();
            connection.Open();

            command = new SqlCommand("select SubjectName from Предмети", connection);

            sqlDataReader = command.ExecuteReader();
            while (sqlDataReader.Read())
            {

                Cb3.Items.Add(sqlDataReader.GetValue(0));

            }
            connection.Close();
            Cb3.SelectedIndex = 0;
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
        private void Сb1_DropDownClosed(object sender, EventArgs e)
        {
            connection.Open();

            command = new SqlCommand("select AmountOfPlaces from Факультети where FacultativeName = '" + Cb1.Text + "'", connection);
            SqlDataReader sqlDataReader = command.ExecuteReader();
            
            while (sqlDataReader.Read())
            {

                amount = Convert.ToInt32(sqlDataReader.GetValue(0));

            }
            connection.Close();
            connection.Open();

            command = new SqlCommand("select top " + amount.ToString() + " AvScore, Surname from Абітуріенти inner join Групи on Групи.IDGroup = Абітуріенти.GroupID inner join Кафедри on Кафедри.IDCathedra = Групи.IDCathedra inner join Факультети on Факультети.IDFacultative = Кафедри.IDFacultative inner join Студенти_Оцінки on Абітуріенти.ExamList = Студенти_Оцінки.IDStudent inner join Предмети on Предмети.IDsubject = Студенти_Оцінки.IDSubject where FacultativeName = '" + Cb1.Text + "' and SubjectName = '" + Cb3.Text + "' order by AvScore desc", connection);
            sqlDataReader = command.ExecuteReader();
            double a = 0;
            List<string> students = new List<string>();
            while (sqlDataReader.Read())
            {
                students.Add(sqlDataReader.GetValue(1).ToString());
                a = Convert.ToDouble(sqlDataReader.GetValue(0));

            }
            connection.Close();
            Lb1.Content = "Прохідний бал по факультету: " + a.ToString();
            Lb2.Content = "К-сть студентів, що пройшли на факультет: " + students.Count.ToString();
            ShowData("select top " + amount.ToString() + " Surname, Абітуріенти.Name, Mark, SubjectName from Абітуріенти inner join Групи on Групи.IDGroup = Абітуріенти.GroupID inner join Кафедри on Кафедри.IDCathedra = Групи.IDCathedra inner join Факультети on Факультети.IDFacultative = Кафедри.IDFacultative inner join Студенти_Оцінки on Абітуріенти.ExamList = Студенти_Оцінки.IDStudent inner join Предмети on Предмети.IDsubject = Студенти_Оцінки.IDSubject where FacultativeName = '" + Cb1.Text + "' and SubjectName = '" + Cb3.Text + "' order by AvScore desc", dataGrid);
            Cb2.SelectedIndex = -1;
        }

        private void Cb2_DropDownClosed(object sender, EventArgs e)
        {
            connection.Open();

            command = new SqlCommand("select AmountOfPlaces, FacultativeName from Факультети inner join Кафедри on Кафедри.IDFacultative = Факультети.IDFacultative where CathedraName = '" + Cb2.Text + "'", connection);
            SqlDataReader sqlDataReader = command.ExecuteReader();
            string facultative = "";
            while (sqlDataReader.Read())
            {

                amount = Convert.ToInt32(sqlDataReader.GetValue(0));
                facultative = sqlDataReader.GetValue(1).ToString();
            }
            connection.Close();
            connection = new SqlConnection(connectionString);

            connection.Open();

            command = new SqlCommand("select top " + amount.ToString() + " Surname, Абітуріенти.Name, Mark, SubjectName, AvScore, CathedraName from Абітуріенти inner join Групи on Групи.IDGroup = Абітуріенти.GroupID inner join Кафедри on Кафедри.IDCathedra = Групи.IDCathedra inner join Факультети on Факультети.IDFacultative = Кафедри.IDFacultative inner join Студенти_Оцінки on Абітуріенти.ExamList = Студенти_Оцінки.IDStudent inner join Предмети on Предмети.IDsubject = Студенти_Оцінки.IDSubject where FacultativeName = '" + facultative + "' and SubjectName = '" + Cb3.Text + "' order by AvScore desc", connection);
            adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            int b = 0;
            double d = 0;
            for (int i = 0; i < table.Rows.Count; i++)
            {
                if (table.Rows[i][5].ToString() != Cb2.Text)
                {
                    table.Rows[i].Delete();
                    b++;
                }
                else
                {
                    d = Convert.ToDouble(table.Rows[i][4]);
                }
            }
            dataGrid.ItemsSource = table.DefaultView;
            Lb1.Content = "Прохідний бал по кафедрі: " + d.ToString();
            Lb2.Content = "К-сть студентів, що пройшли на кафедру: " + (table.Rows.Count - b).ToString();
            connection.Close();
            Cb1.SelectedIndex = -1;
        }

        private void Cb3_DropDownClosed(object sender, EventArgs e)
        {
            if (Cb1.SelectedIndex == -1)
                Cb2_DropDownClosed(sender, e);
            else if (Cb2.SelectedIndex == -1)
                Сb1_DropDownClosed(sender, e);
        }
    }
}
