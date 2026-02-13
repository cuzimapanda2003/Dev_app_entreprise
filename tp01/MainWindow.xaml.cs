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

namespace tp01
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        client c = new client();
        public MainWindow()
        {
            InitializeComponent();
            DataContext = c;
        }


        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            c.Adresse += 's';
            c.Solde += 10.00;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(
                c.Nom + "\n" + c.Solde + "\n" + c.CPR
                );
        }
    }
}
//Le deuxième bouton affiche un messageBox avec le solde et le nom du client 