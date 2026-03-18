using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using WpfPersonne.Commands;
using WpfPersonne.Models;
using WpfPersonne.Services;

namespace WpfPersonne.ViewModels
{
    public class PersonneViewModel : BaseViewModel
    {

        DataServiceFixe ds = DataServiceFixe.GetInstance();

        private Personne selectedPersonne;
       
        public Personne SelectedPersonne
        {
            get => selectedPersonne;
            set
            {
                selectedPersonne = value;
                OnPropertyChanged();
            }

        }
        #region Relaycommand
        public RelayCommand AddPersonneCommand { get; private set; }
       
        public RelayCommand DeletePersonneCommand { get; private set; }
        public RelayCommand FirstPersonneCommand { get; private set; }
        public RelayCommand NextPersonneCommand { get; private set; }
        public RelayCommand LastPersonneCommand { get; private set; }
        public RelayCommand PrevPersonneCommand { get; private set; }
        #endregion

       

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
       




        public PersonneViewModel()
        {
        
            AddPersonneCommand = new RelayCommand(AddPersonne,null);
            DeletePersonneCommand = new RelayCommand(DeletePersonne, null);
                FirstPersonneCommand = new RelayCommand(FirstPersonne, null);
            LastPersonneCommand = new RelayCommand(LastPersonne, null);
            PrevPersonneCommand = new RelayCommand(PrevPersonne, null);
            NextPersonneCommand = new RelayCommand(NextPersonne, null);
            Personnes = ds.Personnes;
            SelectedPersonne = Personnes.FirstOrDefault();
        }

       


        
        
       
        private void AddPersonne(object obj)
        {

            MessageBox.Show("non implémenté");

        }
        private void DeletePersonne(object obj)
        {
            MessageBox.Show("non implémenté");

        }


        #region navigation

        private void NextPersonne(object obj)
        {
            MessageBox.Show("non implémenté");



        }
        private void LastPersonne(object obj)
        {

            MessageBox.Show("non implémenté");

        }
        private void PrevPersonne(object obj)
        {
            MessageBox.Show("non implémenté");


        }
        private void FirstPersonne(object obj)
        {
            MessageBox.Show("non implémenté");
           

        }

        #endregion

    }
}
