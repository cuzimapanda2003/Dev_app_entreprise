using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace tp01
{
    internal class client : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
               => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));


        private int id = 1234;
        private string name = "jean";
        private string telepĥone = "819-123-4567";
        private string adresse = "123 fausse rue";
        private string ville = "Shawinigan";
        private string code_postale = "G2M 1W3";
        private string code_province = "QC";
        private string code_pays = "CAN";
        private float solde = 200;


        public int Id
        {
            get => id;
            set
            {
                id = value;
            }
        }

        public string Nom
        {
            get => name;
            set
            {
                name = value;
                // MessageBox.Show(nom);
                OnPropertyChanged();

            }
        }

        public string Telephone
        {
            get => telepĥone;
            set
            {
                telepĥone = value;
                OnPropertyChanged();

            }
        }

        public string Adresse
        {
            get => adresse;
            set
            {
                adresse = value;
                OnPropertyChanged();

            }
        }

        public string Ville
        {
            get => ville;
            set
            {
                ville = value;
                OnPropertyChanged();

            }
        }

        public string CP
        {
            get => code_postale;
            set
            {
                code_postale = value;
                OnPropertyChanged();

            }
        }

        public string CPR
        {
            get => code_province;
            set
            {
                code_province = value;
                OnPropertyChanged();

            }
        }



        public string CPA
        {
            get => code_pays;
            set
            {
                code_pays = value;
                OnPropertyChanged();

            }
        }

        public float Solde
        {
            get => solde;
            set
            {
                solde = value;
                OnPropertyChanged();
            }
        }




    }
}
