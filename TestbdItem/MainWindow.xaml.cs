using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

namespace TestbdItem
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //string connectionString = @"Data Source=CUZIMAPANDA\SQLEXPRESS;Initial Catalog=bditemcom0ss3;Integrated Security=True;Encrypt=False";
           
            
            //using(SqlConnection connection = new SqlConnection())
            //{
            //    connection.ConnectionString = connectionString;
            //    connection.Open();
            //    connection.Close();
            //  //  txtState.Text = "ouverte";
            //}
            

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnOuvrir_Click(object sender, RoutedEventArgs e)
        {
            txtState.Text = "ouverte";
        }

        private void btnFermer_Click(object sender, RoutedEventArgs e)
        {
            txtState.Text = "fermer";
        }

        private void test_Click(object sender, RoutedEventArgs e)
        {
            txtState.Text = "YIPPIE LE TEST A FONCTIONNER";
        }
    }
}
