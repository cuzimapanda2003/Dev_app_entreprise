using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tpFinal.Commands;

namespace tpFinal.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public RelayCommand CmdGotoAccueil { get; private set; }
        public RelayCommand CmdGotoAventurier { get; private set; }

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

            CurrentViewModel = new AcceuilViewModel();

            CmdGotoAccueil = new RelayCommand(GotoAccueil, null);
            CmdGotoAventurier = new RelayCommand(GotoAventurier, null);
        }
        private void GotoAventurier(object obj)
        {
            CurrentViewModel = new AventurierViewModel();
        }

        private void GotoAccueil(object obj)
        {
            CurrentViewModel = new AcceuilViewModel();
        }
    }
}
