using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WpfPersonne.Models;

namespace WpfPersonne.Services
{
    public class DataServiceFixe : INotifyPropertyChanged
    {

        private static DataServiceFixe instance = null;

        public static DataServiceFixe GetInstance()
        {
            if(instance == null)
            {
                instance = new DataServiceFixe();
            }
            return instance;
        }

        private DataServiceFixe() {
            LoadAll();
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        ObservableCollection<Personne> personnes = new ObservableCollection<Personne>();
        // a ajouter


        public ObservableCollection<Personne> Personnes
        {
            get => personnes;
            set
            {
                personnes = value;
                OnPropertyChanged();

            }
        }


        private void LoadAll()
        {
            LoadPersonnes();

        }





        public void LoadPersonnes()
        {
            Personnes = new ObservableCollection<Personne>
            {
                new Personne(),

                new Personne() { Prenom = "John" },
                new Personne() { Prenom = "Jane", NomFamille = "Doe" },
                new Personne() { Prenom = "Jane", NomFamille = "Doe", Age = 4 },
                new Personne() { Prenom = "Donald", NomFamille = "Doe", Age = 2 },
                new Personne() { Prenom = "Aline", NomFamille = "Dionne", Age = 53 },
                new Personne() { Prenom = "Joseph", NomFamille = "Benoit", Age = 22 },
                new Personne() { Prenom = "Paul", NomFamille = "Doe", Age = 18 }
                 };

            //foreach (var personne in Personnes)
            //{
            //    personne.Province = Provinces.FirstOrDefault(pr => pr.Code_Province == personne.Code_Province);
            //    personne.Profession = Professions.FirstOrDefault(p => p.Id_Profession == personne.Id_Profession);

            //}

        }








    }
}

