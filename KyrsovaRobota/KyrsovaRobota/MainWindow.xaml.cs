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
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window1 window1 = new Window1();
            window1.Show();
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Window2 window2 = new Window2();
            window2.Show();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Window3 window3 = new Window3();
            window3.Show();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Window4 window4 = new Window4();
            window4.Show();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            Window6 window6 = new Window6();
            window6.Show();
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            Window7 window7 = new Window7();
            window7.Show();
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            Window8 window8 = new Window8();
            window8.Show();
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            Window9 window9 = new Window9();
            window9.Show();
        }

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            Window10 window10 = new Window10();
            window10.Show();
        }

        private void Button_Click_9(object sender, RoutedEventArgs e)
        {
            Window11 window11 = new Window11();
            window11.Show();
        }

        private void Button_Click_10(object sender, RoutedEventArgs e)
        {
            Window12 window12 = new Window12();
            window12.Show();
        }

        private void Button_Click_11(object sender, RoutedEventArgs e)
        {
            Window13 window13 = new Window13();
            window13.Show();
        }

        private void Button_Click_12(object sender, RoutedEventArgs e)
        {
            Window14 window14 = new Window14();
            window14.Show();
        }

        private void Button_Click_13(object sender, RoutedEventArgs e)
        {
            Window15 window15 = new Window15();
            window15.Show();
        }

        private void Button_Click_14(object sender, RoutedEventArgs e)
        {
            Window16 window16 = new Window16();
            window16.Show();
        }
    }
}
