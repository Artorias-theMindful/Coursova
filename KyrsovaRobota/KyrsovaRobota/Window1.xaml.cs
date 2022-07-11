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
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        static DataTable table;
        SqlConnection connection = null;
        string connectionString = null;
        SqlCommand command;
        SqlDataAdapter adapter;
        static int id;
        public Window1()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["DefConnection"].ConnectionString;
            connection = new SqlConnection(connectionString);
            connection.Open();

            command = new SqlCommand("select CathedraName from Кафедри", connection);
            SqlDataReader sqlDataReader = command.ExecuteReader();

            while (sqlDataReader.Read())
            {
                Cb2.Items.Add(sqlDataReader.GetValue(0));

            }
            Cb2.SelectedItem = 1;
            
            connection.Close();
            ShowData("SELECT dbo.Абітуріенти.ExamList, dbo.Абітуріенти.Surname, dbo.Абітуріенти.Name, dbo.Абітуріенти.SecName, dbo.Абітуріенти.PassportID, dbo.Абітуріенти.Medal, dbo.Абітуріенти.School, dbo.Абітуріенти.Finished, dbo.Групи.GroupName FROM dbo.Групи INNER JOIN dbo.Абітуріенти ON dbo.Групи.IDGroup = dbo.Абітуріенти.GroupID", dataGrid);

        }
        void ShowData(string SqlQuery, DataGrid dataGrid)
        {
            connection = new SqlConnection(connectionString);

            connection.Open();

            command = new SqlCommand(SqlQuery, connection);
            adapter = new SqlDataAdapter(command);
            table = new DataTable();
            adapter.Fill(table);
            dataGrid.ItemsSource = table.DefaultView;

            connection.Close();
            

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(Cb2.SelectedIndex == -1 || Tb1.Text == "" || Tb2.Text == "" || Tb3.Text == "" || Tb4.Text == "" || Tb4.Text == "" || Tb5.Text == "")
                {
                    MessageBox.Show("Введіть усі необхідні дані");
                    return;
                }
                connection = new SqlConnection(connectionString);
                connection.Open();

                command = new SqlCommand("select count(ExamList) from Абітуріенти", connection);
                SqlDataReader sqlDataReader = command.ExecuteReader();
                int count = 0;
                
                while (sqlDataReader.Read())
                    count = (int)sqlDataReader.GetValue(0);
                connection.Close();
                
                Dictionary<string, int> dict = new Dictionary<string, int>();
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    if (!dict.ContainsKey(table.Rows[i][8].ToString()))
                    {
                        dict.Add(table.Rows[i][8].ToString(), 1);
                    }
                    else
                    {
                        dict[table.Rows[i][8].ToString()]++;
                    }
                }
                List<string> oddKeys = new List<string>();
                string str = "";
                if (dict.Count > 0)
                {
                    int min = dict[table.Rows[0][8].ToString()];
                    oddKeys.Add(table.Rows[0][8].ToString());

                    for (int i = 1; i < table.Rows.Count; i++)
                    {
                        string key = table.Rows[i][8].ToString();
                        if (!oddKeys.Contains(key))
                        {
                            min = Math.Min(min, dict[key]);
                            oddKeys.Add(key);
                        }
                    }

                    foreach (var item in dict)
                    {
                        if (item.Value == min)
                        {
                            str = item.Key;
                        }
                    }
                }
                connection.Open();
                command = new SqlCommand("select IDGroup from Групи where GroupName = '" + str + "'", connection);
                sqlDataReader = command.ExecuteReader();
                int index = 0;
                while (sqlDataReader.Read())
                {
                    index = Convert.ToInt32(sqlDataReader.GetValue(0));
                }
                connection.Close();
                id = getcount();
                connection.Open();
                
                command = new SqlCommand("INSERT INTO dbo.Абітуріенти (ExamList, Surname, Name, SecName, PassportID, School, Finished, Medal, GroupID) VALUES('" + (id + 1).ToString() + "','" + Tb1.Text + "','" + Tb2.Text + "','" + Tb3.Text + "','" + Tb4.Text + "','" + Tb5.Text + "','" + Data.SelectedDate.Value.Year + "-" + Data.SelectedDate.Value.Month + "-" + Data.SelectedDate.Value.Day + "','" + Tb6.Text + "','" + index.ToString() + "' )", connection);
                command.ExecuteNonQuery();
                connection.Close();
                ShowData("SELECT dbo.Абітуріенти.ExamList, dbo.Абітуріенти.Surname, dbo.Абітуріенти.Name, dbo.Абітуріенти.SecName, dbo.Абітуріенти.PassportID, dbo.Абітуріенти.Medal, dbo.Абітуріенти.School, dbo.Абітуріенти.Finished, dbo.Групи.GroupName FROM dbo.Групи INNER JOIN dbo.Абітуріенти ON dbo.Групи.IDGroup = dbo.Абітуріенти.GroupID", dataGrid);
                
                if(Tb6.Text == "Золота" || Tb6.Text == "Срібна")
                {
                    connection.Open();
                    command = new SqlCommand("insert into Студенти_Оцінки (IDStudent, IDSubject) values(" + (id + 1).ToString() + ", 1)", connection);
                    command.ExecuteNonQuery();
                    connection.Close();
                    
                }
                else
                {
                    connection.Open();
                    command = new SqlCommand("insert into Студенти_Оцінки (IDStudent, IDSubject) values(" + (id + 1).ToString() + ", 1)", connection);
                    command.ExecuteNonQuery();
                    connection.Close();
                    connection.Open();
                    command = new SqlCommand("insert into Студенти_Оцінки (IDStudent, IDSubject) values(" + (id + 1).ToString() + ", 2)", connection);
                    command.ExecuteNonQuery();
                    connection.Close();
                    connection.Open();
                    command = new SqlCommand("insert into Студенти_Оцінки (IDStudent, IDSubject) values(" + (id + 1).ToString() + ", 3)", connection);
                    command.ExecuteNonQuery();
                    connection.Close();
                    connection.Open();
                    command = new SqlCommand("insert into Студенти_Оцінки (IDStudent, IDSubject) values(" + (id + 1).ToString() + ", 4)", connection);
                    command.ExecuteNonQuery();
                    connection.Close();
                }
                MessageBox.Show("Абітурієнт успішно доданий!");
                Tb1.Text = "";
                Tb2.Text = "";
                Tb3.Text = "";
                Tb4.Text = "";
                Tb5.Text = "";
                Tb6.Text = "";
                Cb2.SelectedIndex = -1;
            }
            catch(Exception h)
            {
                MessageBox.Show(h.Message);
            }
        }
        
        private int getcount()
        {
            connection.Open();
            command = new SqlCommand("select ExamList from Абітуріенти", connection);
            SqlDataReader sqlDataReader = command.ExecuteReader();
            int count = 0;
            while (sqlDataReader.Read())
            {
                count = int.Parse(sqlDataReader.GetValue(0).ToString());
            }
            connection.Close();
            return count;
        }

        private void Cb2_DropDownClosed(object sender, EventArgs e)
        {
            try
            {

                ShowData("SELECT dbo.Абітуріенти.ExamList, dbo.Абітуріенти.Surname, dbo.Абітуріенти.Name, dbo.Абітуріенти.SecName, dbo.Абітуріенти.PassportID, dbo.Абітуріенти.Medal, dbo.Абітуріенти.School, dbo.Абітуріенти.Finished, dbo.Групи.GroupName FROM dbo.Групи INNER JOIN dbo.Абітуріенти ON dbo.Групи.IDGroup = dbo.Абітуріенти.GroupID inner join Кафедри on Кафедри.IDCathedra = Групи.IDCathedra where CathedraName = '" + Cb2.Text + "'", dataGrid);

            }
            catch
            {

                
            }

           
        }
    }
}
