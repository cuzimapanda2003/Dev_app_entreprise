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
using ViewChangeV2.ViewsModels;

namespace ViewChangeV2
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        private void btn_Red_view_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new RedViewModel();
            //DataContext = new UC_RedView();
        }

        private void btn_Blue_view_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new BlueViewModel();
            //DataContext = new UC_BlueView();
        }

        private void btn_jaune_view_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new JauneViewModel();
        }
    }


}
