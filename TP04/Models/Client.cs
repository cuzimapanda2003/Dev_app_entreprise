using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TP02.Models
{
    public class Client : INotifyPropertyChanged
    {
        //private static int SeqId = 0;
        private decimal solde = 205.23m;
        private string nom = "new client";
        private string ville = "";
        private string telephone = "";
        private string code_postal = "";
        private string code_pays = "CAD";
        private string code_province = "QC";

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // les onchanged pour mettre à jour la liste si saisie avec twoway et UpdateSourceTrigger=PropertyChanged
        public int Id_client { get; set; }
        public Client()
        {
            Id_client = -1;
        }
        public string Nom
        {
            get => nom;
            set
            {
                nom = value;
                OnPropertyChanged();
            }
        }
        public string Adresse { get; set; } = "";
        public decimal Solde
        {
            get => solde;
            set
            {
                solde = value;
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
        public string Telephone
        {
            get => telephone;
            set
            {
                telephone = value;
                OnPropertyChanged();
            }
        }
        public string Code_postal
        {
            get => code_postal;
            set
            {
                code_postal = value;
                OnPropertyChanged();
            }
        }
        public string Code_pays
        {
            get => code_pays;
            set
            {
                code_pays = value;
                OnPropertyChanged();
            }
        }
        public string Code_province
        {
            get => code_province;
            set
            {
                code_province = value;
                OnPropertyChanged();
            }
        }
    }
}
