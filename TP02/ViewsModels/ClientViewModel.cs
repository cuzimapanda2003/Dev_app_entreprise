using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using TP02.Commands;
using TP02.Models;

namespace TP02.ViewsModels
{
    public class ClientViewModel : BaseViewModel
    {

        private Client selected;

        public Client client { get; set; } = new Client();
        public ObservableCollection<Client> clients {  get; set; } = new ObservableCollection<Client>();


        public ClientViewModel() {

            clients = new ObservableCollection<Client>
    {
        new Client
        {
            Id_client = 1,
            Nom = "charle",
            Telephone = "514-111-1111",
            Adresse = "123 Rue Principale",
            Ville = "Montreal",
            Code_postal = "H1A1A1",
            Code_province = "QC",
            Code_pays = "CA",
            Solde = 120
        },
        new Client
        {
            Id_client = 2,
            Nom = "Tremblay",
            Telephone = "514-222-2222",
            Adresse = "456 Boulevard St-Laurent",
            Ville = "Laval",
            Code_postal = "H7A2B2",
            Code_province = "QC",
            Code_pays = "CA",
            Solde = 75
        },
        new Client
        {
            Id_client = 3,
            Nom = "Gagnon",
            Telephone = "514-333-3333",
            Adresse = "789 Avenue des Pins",
            Ville = "Quebec",
            Code_postal = "G1A3C3",
            Code_province = "QC",
            Code_pays = "CA",
            Solde = 200
        },
        new Client
        {
            Id_client = 4,
            Nom = "Roy",
            Telephone = "514-444-4444",
            Adresse = "12 Rue du Parc",
            Ville = "Sherbrooke",
            Code_postal = "J1A4D4",
            Code_province = "QC",
            Code_pays = "CA",
            Solde = 50
        },
        new Client
        {
            Id_client = 5,
            Nom = "Morin",
            Telephone = "514-555-5555",
            Adresse = "98 Chemin du Lac",
            Ville = "Longueuil",
            Code_postal = "J4A5E5",
            Code_province = "QC",
            Code_pays = "CA",
            Solde = 310
        }
    };

            Selected = clients.FirstOrDefault();

            AddCommand = new RelayCommand(addClient, null);
            DeleteCommand = new RelayCommand(deleteClient, null);
            FirstCommand = new RelayCommand(firstClient, null);
            PrevCommand = new RelayCommand(prevClient, null);
            NextCommand = new RelayCommand(nextClient, null);
            LastCommand = new RelayCommand(lastClient, null);


        }

        private void lastClient(object obj)
        {
            Selected = clients.LastOrDefault();
        }

        private void nextClient(object obj)
        {
            if (Selected == null || clients.Count == 0)
                return;

            int index = clients.IndexOf(Selected);

            if (index < clients.Count - 1)
            {
                Selected = clients[index + 1];
            }
        }

        private void prevClient(object obj)
        {
            if (Selected == null || clients.Count == 0)
                return;

            int index = clients.IndexOf(Selected);

            if (index > 0)
            {
                Selected = clients[index - 1];
            }
        }

        private void firstClient(object obj)
        {
            Selected = clients.FirstOrDefault();
        }

        private void deleteClient(object obj)
        {
            clients.Remove(Selected);
            Selected = clients.FirstOrDefault();
        }

        private void addClient(object obj)
        {
            clients.Add(new Client() { Nom = "new" });
            Selected = clients.LastOrDefault();
        }

        public Client Selected
        {
            get => selected;
            set
            {
                selected = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand AddCommand { get; set; }
        public RelayCommand DeleteCommand { get; set; }
        public RelayCommand FirstCommand { get; set; }
        public RelayCommand PrevCommand { get; set; }
        public RelayCommand NextCommand { get; set; }   
        public RelayCommand LastCommand { get; set; }


    }
}
