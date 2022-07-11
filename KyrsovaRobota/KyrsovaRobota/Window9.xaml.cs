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
    /// Логика взаимодействия для Window9.xaml
    /// </summary>
    public partial class Window9 : Window
    {
        SqlConnection connection = null;
        string connectionString = null;
        SqlCommand command;
        
        public Window9()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["DefConnection"].ConnectionString;
            connection = new SqlConnection(connectionString);
        }
        private int PassingScore()
        {
            connection.Open();
            command = new SqlCommand("select AmountOfPlaces, FacultativeName " +
                "from Факультети " +
                "inner join Кафедри on Кафедри.IDFacultative = Факультети.IDFacultative " +
                "inner join Групи on Групи.IDCathedra = Кафедри.IDCathedra " +
                "inner join Абітуріенти on Абітуріенти.GroupID = Групи.IDGroup " +
                "where Surname = '" + Tb1.Text + "'", connection);
            SqlDataReader sqlDataReader = command.ExecuteReader();
            int amount = 0;
            string facultative = "";
            while (sqlDataReader.Read())
            {

                amount = Convert.ToInt32(sqlDataReader.GetValue(0));
                facultative = sqlDataReader.GetValue(1).ToString();
            }
            connection.Close();
            connection.Open();
            
            command = new SqlCommand("select top " + amount.ToString() + " Surname from Абітуріенти inner join Групи on Групи.IDGroup = Абітуріенти.GroupID inner join Кафедри on Кафедри.IDCathedra = Групи.IDCathedra inner join Факультети on Факультети.IDFacultative = Кафедри.IDFacultative where FacultativeName = '" + facultative + "' order by AvScore desc", connection);
            sqlDataReader = command.ExecuteReader();
            List<string> surnames = new List<string>();
            while (sqlDataReader.Read())
            {
                surnames.Add(sqlDataReader.GetValue(0).ToString());
            }
            connection.Close();
            connection.Open();
            command = new SqlCommand("select SubjectName, Mark from Студенти_Оцінки inner join Абітуріенти on Абітуріенти.ExamList = Студенти_Оцінки.IDStudent inner join Предмети on Предмети.IDsubject = Студенти_Оцінки.IDSubject where Surname = '" + Tb1.Text + "'", connection);
            sqlDataReader = command.ExecuteReader();
            List<string> subjects = new List<string>();
            List<string> marks = new List<string>();
            
            while (sqlDataReader.Read())
            {
                subjects.Add(sqlDataReader.GetValue(0).ToString());
                marks.Add(sqlDataReader.GetValue(1).ToString());
                
            }
            if(subjects.Count == 0)
            {
                connection.Close();
                MessageBox.Show("Такого прізвища немає");
                return 0;
            }
            if (surnames.Contains(Tb1.Text))
            {
                Label.Content = "Вітаємо! Ви пройшли на факультет " + facultative + ". Ваші оцінки такі:\n";
                for(int i = 0; i < subjects.Count; i++)
                {
                    Label.Content += subjects[i] + " - " + marks[i] + "\n";
                }
            }
            else
            {
                Label.Content = "Вибачте! Ви не пройшли на факультет " + facultative + ". Ваші оцінки такі:\n";
                for (int i = 0; i < subjects.Count; i++)
                {
                    Label.Content += subjects[i] + " - " + marks[i] + "\n";
                }
            }
            connection.Close();
            return amount;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PassingScore();
        }
    }
}
