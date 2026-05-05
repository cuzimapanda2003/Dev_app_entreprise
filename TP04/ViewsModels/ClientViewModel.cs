using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using TP02.Commands;
using TP02.Models;
using TP02.Service;

namespace TP02.ViewsModels
{
    public class ClientViewModel : BaseViewModel
    {
        public Client client { get; set; } = new Client();

        private ObservableCollection<Client> clients = new ObservableCollection<Client>();

        public ObservableCollection<Client> Clients
        {
            get => clients;
            set
            {
                clients = value;
                OnPropertyChanged();

            }
        }

        DataService ds = DataService.GetInstance();

        private ICollectionView clientsViewSource;
        private ICollectionView paysViewSource;
        private ICollectionView provincesViewSource;

        public ICollectionView ClientsViewSource
        {
            get => clientsViewSource;
            set
            {
                clientsViewSource = value;
                OnPropertyChanged();
            }
        }

        public ICollectionView PaysViewSource
        {
            get => paysViewSource;
            set
            {
                paysViewSource = value;
                OnPropertyChanged();
            }
        }

        public ICollectionView ProvincesViewSource
        {
            get => provincesViewSource;
            set
            {
                provincesViewSource = value;
                OnPropertyChanged();
            }
        }



        public ClientViewModel()
        {
            ClientsViewSource = CollectionViewSource.GetDefaultView(ds.Clients);
            PaysViewSource = CollectionViewSource.GetDefaultView(ds.Pays);
            ProvincesViewSource = CollectionViewSource.GetDefaultView(ds.Provinces);

            AddCommand = new RelayCommand(addClient, CanBeginEdit);
            DeleteCommand = new RelayCommand(deleteClient, CanBeginEdit);
            EditCommand = new RelayCommand(editClient, CanBeginEdit);
            SaveCommand = new RelayCommand(saveClient, CanEndEdit);
            CancelCommand = new RelayCommand(cancelClient, CanEndEdit);





            FirstCommand = new RelayCommand(firstClient, CanBeginEdit);
            PrevCommand = new RelayCommand(prevClient, CanBeginEdit);
            NextCommand = new RelayCommand(nextClient, CanBeginEdit);
            LastCommand = new RelayCommand(lastClient, CanBeginEdit);
        }

 

        private void editClient(object obj)
        {
            ActionModeActuel = ACTIONMODE.EDIT;
        }

        private void lastClient(object obj)
        {
            ClientsViewSource.MoveCurrentToLast();
        }

        private void nextClient(object obj)
        {
            ClientsViewSource.MoveCurrentToNext();

            if (ClientsViewSource.IsCurrentAfterLast)
            {
                ClientsViewSource.MoveCurrentToPrevious();
            }

        }

        private void prevClient(object obj)
        {
            ClientsViewSource.MoveCurrentToPrevious();
            if (ClientsViewSource.IsCurrentBeforeFirst)
            {
                ClientsViewSource.MoveCurrentToFirst();
            }

        }

        private void firstClient(object obj)
        {
            ClientsViewSource.MoveCurrentToFirst();
        }



       private void cancelClient(object obj)
        {
            Client client = (Client)ClientsViewSource.CurrentItem;
            if (ActionModeActuel == ACTIONMODE.ADD)
            {
                ds.Clients.Remove(client);
                ClientsViewSource.Refresh();
            }
            else
            {
                ds.Reload(client);
                ClientsViewSource.Refresh();
                ClientsViewSource.MoveCurrentTo(null);
                ClientsViewSource.MoveCurrentTo(client);
            }
        }

        private void saveClient(object obj)
        {
            Client client = (Client)ClientsViewSource.CurrentItem;
            if (client != null)
            { // validations ici pour le moment
                ds.Save(client);

                // maj des controles autres que liste si nous modifions
                // une valeur par code et non par saisie à l'écran
                // simulation de clic sur un autre et revient dessus
                ClientsViewSource.MoveCurrentTo(null);
                ClientsViewSource.MoveCurrentTo(client);
                // maj de des listes ex lisxbox
                ClientsViewSource.Refresh();
            }

            ActionModeActuel = ACTIONMODE.DISPLAY;
        }
        private void deleteClient(object obj)
        {
            if (ClientsViewSource.CurrentItem != null)
            {
                string messageBoxText = "Etes-vous certain de vouloir supprimer cette Personne?";
                string caption = "Vous etes sur le point de détruire une Personne";
                MessageBoxButton button = MessageBoxButton.OKCancel;
                MessageBoxImage icon = MessageBoxImage.Warning;

                MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);

                if (result == MessageBoxResult.OK)
                {
                    //Personnes.Remove(selectedPersonne);
                    //SelectedPersonne = Personnes.FirstOrDefault();
                    Client client = (Client)ClientsViewSource.CurrentItem;
                    ds.Delete(client);

                    ds.Clients.Remove(client);
                    ClientsViewSource.Refresh();

                    // ds.Delete(personne);  // on ne peut pas faire de remove sur une vue

                }
            }
            else
            {
                MessageBox.Show("Vous devez sélectionner une personne");
            }
        }

        private void addClient(object obj)
        {
            ActionModeActuel = ACTIONMODE.ADD;
            Client client = new Client();
            // impossible d'ajouter dans une vue
            ds.Clients.Add(client);
            ClientsViewSource.MoveCurrentTo(client);
        }



        public RelayCommand AddCommand { get; set; }
        public RelayCommand DeleteCommand { get; set; }
        public RelayCommand EditCommand { get; set; }
        public RelayCommand SaveCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }
        public RelayCommand FirstCommand { get; set; }
        public RelayCommand PrevCommand { get; set; }
        public RelayCommand NextCommand { get; set; }
        public RelayCommand LastCommand { get; set; }


    }
}
