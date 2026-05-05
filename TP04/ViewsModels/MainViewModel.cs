using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TP02.Commands;

namespace TP02.ViewsModels
{
    public class MainViewModel : BaseViewModel
    {
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


        public RelayCommand AcceuilCommand { get; set; }
        public RelayCommand ClientCommand { get; set; }

        public MainViewModel()
        {
            AcceuilCommand = new RelayCommand(GotoAcceuil, null);
            ClientCommand = new RelayCommand(GotoClient, null);

        }

        private void GotoClient(object obj)
        {
            CurrentViewModel = new ClientViewModel();
        }

        private void GotoAcceuil(object obj)
        {
            CurrentViewModel = new AcceuilViewModel();
        }
    }
}
