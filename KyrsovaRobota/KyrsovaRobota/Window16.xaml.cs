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
    /// Логика взаимодействия для Window16.xaml
    /// </summary>
    public partial class Window16 : Window
    {
        SqlConnection connection = null;
        string connectionString = null;
        SqlCommand command;
        SqlDataAdapter adapter;
        public Window16()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["DefConnection"].ConnectionString;
            connection = new SqlConnection(connectionString);
            connection.Open();
            command = new SqlCommand("select FacultativeName from Факультети", connection);
            SqlDataReader sqlDataReader = command.ExecuteReader();
            List<string> strFac = new List<string>();
            while (sqlDataReader.Read())
            {
                strFac.Add(sqlDataReader.GetValue(0).ToString());
            }
            connection.Close();
            connection.Open();
            command = new SqlCommand("select SubjectName from Предмети", connection);
            sqlDataReader = command.ExecuteReader();
            List<string> str = new List<string>();
            while (sqlDataReader.Read())
            {
                str.Add(sqlDataReader.GetValue(0).ToString());
            }
            connection.Close();

            for (int i = 0; i < strFac.Count; i++)
            {
                Lb1.Content += strFac[i] + ":\n";
                for (int j = 0; j < str.Count; j++)
                {
                    
                    connection.Open();
                    command = new SqlCommand("select avg(Mark) from Студенти_Оцінки inner join Абітуріенти on Абітуріенти.ExamList = Студенти_Оцінки.IDStudent inner join Групи on Групи.IDGroup = Абітуріенти.GroupID inner join Кафедри on Кафедри.IDCathedra = Групи.IDCathedra inner join Факультети on Факультети.IDFacultative = Кафедри.IDFacultative where Факультети.IDFacultative = " + (i + 1).ToString() +" and Студенти_Оцінки.IDSubject = " + (j + 1).ToString(), connection);
                    sqlDataReader = command.ExecuteReader();
                    double avr = 0;
                    while (sqlDataReader.Read())
                    {
                        avr = Convert.ToDouble(sqlDataReader.GetValue(0));
                    }
                    connection.Close();
                    Lb1.Content += str[j] + " - " + avr.ToString() + "\n";
                }
            }
        }
    }
}
