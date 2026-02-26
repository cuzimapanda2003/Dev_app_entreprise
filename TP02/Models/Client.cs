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
        private decimal solde = 205.23m;
        private string nom = "Margerite LaFleur";

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public int Id_client { get; set; } = 12345;
        public string Nom
        {
            get => nom;
            set
            {
                nom = value;
                OnPropertyChanged();
            }
        }
        public string Adresse { get; set; } = "12 rose";
        public decimal Solde
        {
            get => solde;
            set
            {
                solde = value;
                OnPropertyChanged();
            }
        }
        public string Ville { get; set; } = "Fleurimont";
        public string Telephone { get; set; } = "8195381234";
        public string Code_postal { get; set; } = "G1R 0PT";
        public string Code_pays { get; set; } = "CAD";
        public string Code_province { get; set; } = "QC";
        public Client() { }

    }
}
