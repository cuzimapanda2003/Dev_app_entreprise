using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using tpFinal.Commands;
using tpFinal.Models;
using tpFinal.Services;

namespace tpFinal.ViewModels
{
    public class AventurierViewModel : BaseViewModel
    {
        DataService ds = DataService.GetInstance();

        private ICollectionView aventuriersViewSource;
        private ICollectionView metiersViewSource;
        private ICollectionView equipementsViewSource;

        public ICollectionView AventuriersViewSource
        {
            get => aventuriersViewSource;
            set { aventuriersViewSource = value; OnPropertyChanged(); }
        }

        public ICollectionView MetiersViewSource
        {
            get => metiersViewSource;
            set { metiersViewSource = value; OnPropertyChanged(); }
        }

        public ICollectionView EquipementsViewSource
        {
            get => equipementsViewSource;
            set { equipementsViewSource = value; OnPropertyChanged(); }
        }

        private Visibility visibilityListSelectEquipement = Visibility.Collapsed;
        public Visibility VisibilityListSelectEquipement
        {
            get => visibilityListSelectEquipement;
            set { visibilityListSelectEquipement = value; OnPropertyChanged(); }
        }

        public RelayCommand AddAventurierCommand { get; private set; }
        public RelayCommand EditAventurierCommand { get; private set; }
        public RelayCommand DeleteAventurierCommand { get; private set; }
        public RelayCommand SaveAventurierCommand { get; private set; }
        public RelayCommand CancelAventurierCommand { get; private set; }
        public RelayCommand FirstAventurierCommand { get; private set; }
        public RelayCommand PrevAventurierCommand { get; private set; }
        public RelayCommand NextAventurierCommand { get; private set; }
        public RelayCommand LastAventurierCommand { get; private set; }

        public RelayCommand AddEquipementCommand { get; private set; }
        public RelayCommand DeleteEquipementCommand { get; private set; }
        public RelayCommand AddSelectEquipementCommand { get; private set; }
        public RelayCommand CancelSelectEquipementCommand { get; private set; }

        public AventurierViewModel()
        {
            AventuriersViewSource = CollectionViewSource.GetDefaultView(ds.Aventuriers);
            MetiersViewSource = CollectionViewSource.GetDefaultView(ds.Metiers);
            EquipementsViewSource = CollectionViewSource.GetDefaultView(ds.Equipements);

            AddAventurierCommand = new RelayCommand(AddAventurier, CanBeginEdit);
            EditAventurierCommand = new RelayCommand(EditAventurier, CanBeginEdit);
            DeleteAventurierCommand = new RelayCommand(DeleteAventurier, CanBeginEdit);
            SaveAventurierCommand = new RelayCommand(SaveAventurier, CanEndEdit);
            CancelAventurierCommand = new RelayCommand(CancelAventurier, CanEndEdit);
            FirstAventurierCommand = new RelayCommand(FirstAventurier, CanBeginEdit);
            PrevAventurierCommand = new RelayCommand(PrevAventurier, CanBeginEdit);
            NextAventurierCommand = new RelayCommand(NextAventurier, CanBeginEdit);
            LastAventurierCommand = new RelayCommand(LastAventurier, CanBeginEdit);

            AddEquipementCommand = new RelayCommand(AddEquipement, CanEndEdit);
            DeleteEquipementCommand = new RelayCommand(DeleteEquipement, CanEndEdit);
            AddSelectEquipementCommand = new RelayCommand(addSelectedEquipement, null);
            CancelSelectEquipementCommand = new RelayCommand(cancelSelectedEquipement, null);
        }

        private void AddAventurier(object obj)
        {
            ActionModeActuel = ACTIONMODE.ADD;
            Aventurier aventurier = new Aventurier();
            ds.Aventuriers.Add(aventurier);
            AventuriersViewSource.MoveCurrentTo(aventurier);
        }

        private void EditAventurier(object obj)
        {
            ActionModeActuel = ACTIONMODE.EDIT;
        }

        private void DeleteAventurier(object obj)
        {
            if (AventuriersViewSource.CurrentItem != null)
            {
                MessageBoxResult result = MessageBox.Show(
                    "Êtes-vous certain de vouloir supprimer cet aventurier?",
                    "Suppression",
                    MessageBoxButton.OKCancel,
                    MessageBoxImage.Warning);

                if (result == MessageBoxResult.OK)
                {
                    Aventurier aventurier = (Aventurier)AventuriersViewSource.CurrentItem;
                    ds.Delete(aventurier);
                }
            }
            else
            {
                MessageBox.Show("Vous devez sélectionner un aventurier.");
            }
        }

        private void SaveAventurier(object obj)
        {
            Aventurier aventurier = (Aventurier)AventuriersViewSource.CurrentItem;
            if (aventurier != null)
            {
                if (aventurier.nom.Length < 2)
                {
                    MessageBox.Show("Entrez un nom valide.");
                    return;
                }
                ds.Save(aventurier);

                AventuriersViewSource.MoveCurrentTo(null);
                AventuriersViewSource.MoveCurrentTo(aventurier);
                AventuriersViewSource.Refresh();
            }
            ActionModeActuel = ACTIONMODE.DISPLAY;
        }

        private void CancelAventurier(object obj)
        {
            Aventurier aventurier = (Aventurier)AventuriersViewSource.CurrentItem;

            if (ActionModeActuel == ACTIONMODE.ADD)
            {
                ds.Aventuriers.Remove(aventurier);
                AventuriersViewSource.Refresh();
            }
            else
            {
                ds.Reload(aventurier);
                AventuriersViewSource.Refresh();
                AventuriersViewSource.MoveCurrentTo(null);
                AventuriersViewSource.MoveCurrentTo(aventurier);
            }
            ActionModeActuel = ACTIONMODE.DISPLAY;
        }

        private void AddEquipement(object obj)
        {
            VisibilityListSelectEquipement = Visibility.Visible;
        }

        private void DeleteEquipement(object obj)
        {
            Aventurier a = (Aventurier)AventuriersViewSource.CurrentItem;
            if (obj is AventurierEquipement)
            {
                AventurierEquipement ae = (AventurierEquipement)obj;

                MessageBoxResult result = MessageBox.Show(
                    "Supprimer cet équipement?", "Confirmation",
                    MessageBoxButton.OKCancel, MessageBoxImage.Warning);

                if (result == MessageBoxResult.OK)
                {
                    if (ActionModeActuel == ACTIONMODE.EDIT && ae.id > 0)
                        a.ListEquipementsToDelete.Add(ae);

                    a.AventurierEquipements.Remove(ae);
                }
            }
            else
            {
                MessageBox.Show("Sélectionnez un équipement à supprimer.");
            }
        }

        private void addSelectedEquipement(object obj)
        {
            VisibilityListSelectEquipement = Visibility.Collapsed;

            if (EquipementsViewSource.CurrentItem != null)
            {
                Aventurier a = (Aventurier)AventuriersViewSource.CurrentItem;
                Equipement e = (Equipement)EquipementsViewSource.CurrentItem;

                AventurierEquipement ae = new AventurierEquipement();
                ae.id = -1;
                ae.id_aventurier = a.id;
                ae.id_equipement = e.id;
                ae.quantite_totale = 1;
                ae.quantite_active = 1;
                ae.prix_achat = e.prix_unitaire;
                ae.Equipement = e;

                a.AventurierEquipements.Add(ae);
            }
        }

        private void cancelSelectedEquipement(object obj)
        {
            VisibilityListSelectEquipement = Visibility.Collapsed;
        }

        private void FirstAventurier(object obj) => AventuriersViewSource.MoveCurrentToFirst();
        private void LastAventurier(object obj) => AventuriersViewSource.MoveCurrentToLast();

        private void NextAventurier(object obj)
        {
            AventuriersViewSource.MoveCurrentToNext();
            if (AventuriersViewSource.IsCurrentAfterLast)
                AventuriersViewSource.MoveCurrentToPrevious();
        }

        private void PrevAventurier(object obj)
        {
            AventuriersViewSource.MoveCurrentToPrevious();
            if (AventuriersViewSource.IsCurrentBeforeFirst)
                AventuriersViewSource.MoveCurrentToFirst();
        }
    }
}