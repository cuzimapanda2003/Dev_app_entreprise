using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
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

namespace datacontext
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Personne p1 = new Personne();
        //aurait pus faire public Personne p1 {get; set;} = new Personne();
        //et utiliser DataContext = this, mais dans .xaml devrais utiliser p1.age au lieux de age
        public MainWindow()
        {
            DataContext = p1;
            InitializeComponent();
        }





    }
}
