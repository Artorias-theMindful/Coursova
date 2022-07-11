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
    /// Логика взаимодействия для Window6.xaml
    /// </summary>
    public partial class Window6 : Window
    {
        SqlConnection connection = null;
        string connectionString = null;
        SqlCommand command;
        SqlDataAdapter adapter;
        static DataTable table;
        public Window6()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["DefConnection"].ConnectionString;
            connection = new SqlConnection(connectionString);
            ShowData("SELECT dbo.Абітуріенти.ExamList, dbo.Абітуріенти.Surname, dbo.Абітуріенти.Name, dbo.Абітуріенти.SecName, dbo.Групи.GroupName FROM dbo.Групи INNER JOIN dbo.Абітуріенти ON dbo.Групи.IDGroup = dbo.Абітуріенти.GroupID", dataGrid);

            Update();
        }
        void Update()
        {
            Cb2.Items.Clear();
            Cb3.Items.Clear();
            connection.Open();
            Cb1.Items.Clear();
            command = new SqlCommand("select GroupName from Групи", connection);
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

                Cb3.Items.Add(sqlDataReader.GetValue(0));

            }
            connection.Close();
        }
        private void Cb1_DropDownClosed(object sender, EventArgs e)
        {
            Cb2.Items.Clear();
            connection.Open();

            Cb2.IsEnabled = true;

            command = new SqlCommand("select Surname from Абітуріенти inner join Групи on Абітуріенти.GroupID = Групи.IDGroup where  GroupName = '" + ((sender as ComboBox).SelectedItem as string) + "'", connection);
            SqlDataReader sqlDataReader = command.ExecuteReader();


            while (sqlDataReader.Read())
            {
                if (!Cb2.Items.Contains(sqlDataReader.GetValue(0)))
                    Cb2.Items.Add(sqlDataReader.GetValue(0));
            }
            connection.Close();
        }

        

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Dictionary<string, int> dict = new Dictionary<string, int>();
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    if (!dict.ContainsKey(table.Rows[i][4].ToString()))
                    {
                        dict.Add(table.Rows[i][4].ToString(), 1);
                    }
                    else
                    {
                        dict[table.Rows[i][4].ToString()]++;
                    }
                }
                List<string> oddKeys = new List<string>();
                string str = "";
                if (dict.Count > 0)
                {
                    int min = dict[table.Rows[0][4].ToString()];
                    oddKeys.Add(table.Rows[0][4].ToString());

                    for (int i = 1; i < table.Rows.Count; i++)
                    {
                        string key = table.Rows[i][4].ToString();
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
                SqlDataReader sqlDataReader = command.ExecuteReader();
                int id = 0;
                while (sqlDataReader.Read())
                {
                    id = Convert.ToInt32(sqlDataReader.GetValue(0));
                }
                connection.Close();
                connection.Open();
                command = new SqlCommand("update Абітуріенти set GroupID = " + id.ToString() + " where Surname = '" + Cb2.Text + "'", connection);
                command.ExecuteNonQuery();
                connection.Close();
                ShowData("SELECT dbo.Абітуріенти.ExamList, dbo.Абітуріенти.Surname, dbo.Абітуріенти.Name, dbo.Абітуріенти.SecName, dbo.Групи.GroupName FROM dbo.Групи INNER JOIN dbo.Абітуріенти ON dbo.Групи.IDGroup = dbo.Абітуріенти.GroupID", dataGrid);
                MessageBox.Show("Переказ абітуріента на іншу кафедру успішний");
            }
            catch
            {
                MessageBox.Show("Виберіть потрібні дані");
            }
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

        private void Cb3_DropDownClosed(object sender, EventArgs e)
        {
            ShowData("SELECT dbo.Абітуріенти.ExamList, dbo.Абітуріенти.Surname, dbo.Абітуріенти.Name, dbo.Абітуріенти.SecName, dbo.Групи.GroupName FROM dbo.Групи INNER JOIN dbo.Абітуріенти ON dbo.Групи.IDGroup = dbo.Абітуріенти.GroupID inner join Кафедри on Кафедри.IDCathedra = Групи.IDCathedra where CathedraName = '" + Cb3.Text + "'", dataGrid);

        }
    }
}
