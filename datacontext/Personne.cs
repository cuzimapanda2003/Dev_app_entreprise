using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace datacontext
{
    public class Personne : Window, INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
               => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));




        private string nom = "patate";
        private string prenom = "pillé";
        private int age = 18;

        public string Nom
        {
            get => nom;
            set
            {
                nom = value;
                MessageBox.Show(nom);
                OnPropertyChanged();
                OnPropertyChanged("NomComplet");

            }
        }
        public string Prenom
        {
            get => prenom;
            set
            {
                prenom = value;
                MessageBox.Show(prenom);
                OnPropertyChanged();
                OnPropertyChanged("NomComplet");
            }
        }
        public int Age
        {
            get => age;
            set
            {
                age = value;
                MessageBox.Show(age.ToString());
            }
        }
        public string NomComplet
        {
            get
            {
                return $" {Prenom}  {Nom} ";
            }
        }

    }
}
