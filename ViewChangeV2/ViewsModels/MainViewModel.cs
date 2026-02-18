using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ViewChangeV2.Commands;

namespace ViewChangeV2.ViewsModels
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
        public RelayCommand RedViewCommand { get; set; }
        public RelayCommand BlueViewCommand { get; set; }
        public RelayCommand YellowViewCommand { get; set; }

        public MainViewModel()
        {
           // CurrentViewModel = new RedViewModel();

            RedViewCommand = new RelayCommand(GotoRed, null);
            BlueViewCommand = new RelayCommand(GotoBlue, null);
            YellowViewCommand = new RelayCommand(GotoJaune, null);
      

        }

        private void GotoJaune(object obj)
        {
            CurrentViewModel = new JauneViewModel();
        }

        private void GotoBlue(object obj)
        {
            CurrentViewModel = new BlueViewModel();
        }

        private void GotoRed(object obj)
        {
            CurrentViewModel = new RedViewModel();
        }
    }
}
