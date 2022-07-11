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
    /// Логика взаимодействия для Window4.xaml
    /// </summary>
    public partial class Window4 : Window
    {
        SqlConnection connection = null;
        string connectionString = null;
        SqlCommand command;
        
        public Window4()
        {
            InitializeComponent();
            
            connectionString = ConfigurationManager.ConnectionStrings["DefConnection"].ConnectionString;
            connection = new SqlConnection(connectionString);
            Update();
            
        }
        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                connection.Open();

                command = new SqlCommand("DELETE FROM dbo.Абітуріенти WHERE Surname = '" + Cb2.Text + "'", connection);
                command.ExecuteNonQuery();
                MessageBox.Show("Документи абітурієнта віддані");

                connection.Close();
            }
            catch(Exception h)
            {
                MessageBox.Show(h.Message);
            }
            Update();
        }
        void Update()
        {
            Cb2.Items.Clear();
            connection.Open();
            Cb1.Items.Clear();
            command = new SqlCommand("select GroupName from Групи", connection);
            SqlDataReader sqlDataReader = command.ExecuteReader();
            
            while (sqlDataReader.Read())
            {
                
                Cb1.Items.Add(sqlDataReader.GetValue(0));

            }
            connection.Close();
        }
        #region 
        
        #endregion

        private void Cb1_SelectionChanged(object sender, EventArgs e)
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
    }
}
