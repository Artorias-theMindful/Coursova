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
    /// Логика взаимодействия для Window15.xaml
    /// </summary>
    public partial class Window15 : Window
    {
        SqlConnection connection = null;
        string connectionString = null;
        SqlCommand command;
        SqlDataAdapter adapter;
        public Window15()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["DefConnection"].ConnectionString;
            connection = new SqlConnection(connectionString);
            connection.Open();
            

            command = new SqlCommand("select COUNT(FacultativeName) from Факультети", connection);
            SqlDataReader sqlDataReader = command.ExecuteReader();
            int count = 0;
            while (sqlDataReader.Read())
            {
                count = Convert.ToInt32(sqlDataReader.GetValue(0));
            }
            connection.Close();
            for (int i = 0; i < count; i++)
            {
                connection.Open();
                command = new SqlCommand("select count(Surname) from Абітуріенти inner join Групи on Групи.IDGroup = Абітуріенти.GroupID inner join Кафедри on Кафедри.IDCathedra = Групи.IDCathedra inner join Факультети on Факультети.IDFacultative = Кафедри.IDFacultative where Факультети.IDFacultative = " + (i + 1).ToString(), connection);
                int studentsCount = 0;
                sqlDataReader = command.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    studentsCount = Convert.ToInt32(sqlDataReader.GetValue(0));
                }
                connection.Close();
                connection.Open();
                command = new SqlCommand("select FacultativeName, AmountOfPlaces from Факультети where IDFacultative = " + (i + 1).ToString(), connection);
                int acceptedCount = 0;
                sqlDataReader = command.ExecuteReader();
                string str = "";
                while (sqlDataReader.Read())
                {
                    acceptedCount = Convert.ToInt32(sqlDataReader.GetValue(1));
                    str = sqlDataReader.GetValue(0).ToString();
                }
                double concurs = Convert.ToDouble(studentsCount) / Convert.ToDouble(acceptedCount);
                Lb1.Content += str + " - " + concurs + "\n";
                connection.Close();
            }
            
        }
    }
}
