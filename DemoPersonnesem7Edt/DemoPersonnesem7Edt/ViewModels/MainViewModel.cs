using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfPersonne.Commands;// ici
using WpfPersonne.Models;

namespace WpfPersonne.ViewModels
{
    public class MainViewModel  :BaseViewModel
    {   
       
        
        public RelayCommand CmdGotoAccueil { get; private set; }
        public RelayCommand CmdGotoPersonne { get; private set; }

        private BaseViewModel currentViewModel;

        public BaseViewModel CurrentViewModel
        {
            get => currentViewModel;
            set
            {
                currentViewModel = value;
                OnPropertyChanged();
            }

        }
        public MainViewModel()
        { 
  
            CurrentViewModel = new AccueilViewModel();
            // le contentControl doit savoir afficher un AccueilViewModel

            CmdGotoAccueil = new RelayCommand(GotoAccueil, null);
            CmdGotoPersonne = new RelayCommand(GotoPersonne, null);
        }
        private void GotoPersonne(object obj)
        {
            CurrentViewModel = new PersonneViewModel();
        }

        private void GotoAccueil(object obj)
        {
            CurrentViewModel = new AccueilViewModel();
        }
    }
}
