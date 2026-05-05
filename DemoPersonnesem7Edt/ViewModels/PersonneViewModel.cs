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
        DataService ds = DataService.GetInstance();

        private ICollectionView personnesViewSource;
        private ICollectionView professionsViewSource;
        private ICollectionView provincesViewSource;
        private ICollectionView chaussuresViewSource;
        private Visibility visibilityListSelectChaussure = Visibility.Collapsed;

        public Visibility VisibilityListSelectChaussure
        {
            get => visibilityListSelectChaussure;
            set
            {
                visibilityListSelectChaussure = value;
                OnPropertyChanged();
            }
        }

        //private Personne selectedPersonne;

        //public Personne SelectedPersonne
        //{
        //    get => selectedPersonne;
        //    set
        //    {
        //        selectedPersonne = value;
        //        OnPropertyChanged();
        //    }

        //}

        public ICollectionView ProfessionsViewSource
        {
            get => professionsViewSource;
            set
            {
                professionsViewSource = value;
                OnPropertyChanged();
            }
        }

        public ICollectionView PersonnesViewSource
        {
            get => personnesViewSource;
            set
            {
                personnesViewSource = value;
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

        public ICollectionView ChaussuresViewSource
        {
            get => chaussuresViewSource;
            set
            {
                chaussuresViewSource = value;
                OnPropertyChanged();
            }
        }
 
        public RelayCommand AddPersonneCommand { get; private set; }
       
        public RelayCommand DeletePersonneCommand { get; private set; }
        public RelayCommand ModifParCodeCommand { get; set; }
        public RelayCommand FirstPersonneCommand { get; private set; }
        public RelayCommand NextPersonneCommand { get; private set; }
        public RelayCommand LastPersonneCommand { get; private set; }
        public RelayCommand PrevPersonneCommand { get; private set; }
        public RelayCommand EditPersonneCommand { get; private set; }
        public RelayCommand SavePersonneCommand { get; private set; }
        public RelayCommand CancelPersonneCommand { get; private set; }
        public RelayCommand AddEnfantCommand {  get; private set; }
        public RelayCommand DeleteEnfantCommand {get; private set; }
        public RelayCommand AddSelectChaussureCommand {  get; private set; }
        public RelayCommand CancelSelectChaussureCommand { get; private set; }







        //   ObservableCollection<Personne> personnes = new ObservableCollection<Personne>(); 

        //   public ObservableCollection<Personne> Personnes
        //   {
        //       get => personnes;
        //       set
        //       {
        //           personnes = value;
        //           OnPropertyChanged();

        //       }
        //   }

        //   ObservableCollection<Profession> professions = new ObservableCollection<Profession>();

        //   public ObservableCollection<Profession> Professions
        //   {
        //       get => professions;
        //       set
        //       {
        //           professions = value;
        //           OnPropertyChanged();
        //       }
        //   }

        //   private ObservableCollection<Province> provinces;

        //   public ObservableCollection<Province> Provinces
        //   {
        //       get { return provinces; }
        //       set
        //       {
        //           provinces = value;
        //           OnPropertyChanged();
        //       }
        //   }




        public PersonneViewModel()
        {
            
            PersonnesViewSource = CollectionViewSource.GetDefaultView(ds.Personnes);
            ProfessionsViewSource = CollectionViewSource.GetDefaultView(ds.Professions);
            ProvincesViewSource = CollectionViewSource.GetDefaultView(ds.Provinces);
            ChaussuresViewSource = CollectionViewSource.GetDefaultView(ds.Chaussures);

            AddPersonneCommand = new RelayCommand(AddPersonne, CanBeginEdit);
            DeletePersonneCommand = new RelayCommand(DeletePersonne, CanBeginEdit);
            EditPersonneCommand = new RelayCommand(EditPersonne, CanBeginEdit);

            SavePersonneCommand = new RelayCommand(SavePersonne, CanEndEdit);
            CancelPersonneCommand = new RelayCommand(CancelPersonne, CanEndEdit);
            FirstPersonneCommand = new RelayCommand(FirstPersonne, CanBeginEdit);
            LastPersonneCommand = new RelayCommand(LastPersonne, CanBeginEdit);
            PrevPersonneCommand = new RelayCommand(PrevPersonne, CanBeginEdit);
            NextPersonneCommand = new RelayCommand(NextPersonne, CanBeginEdit);
            SavePersonneCommand = new RelayCommand(SavePersonne, CanEndEdit);
            AddEnfantCommand = new RelayCommand(AddChaussure, CanEndEdit);
            DeleteEnfantCommand = new RelayCommand(KillChild, CanEndEdit);
            CancelSelectChaussureCommand = new RelayCommand(cancelSelectedChaussure, null);
            AddSelectChaussureCommand = new RelayCommand(addSelectedChaussure, null);




            //Personnes = ds.Personnes;
            //Professions = ds.Professions;
            //Provinces = ds.Provinces;
            //SelectedPersonne = Personnes.FirstOrDefault();
        }

        private void addSelectedChaussure(object obj)
        {
            VisibilityListSelectChaussure = Visibility.Collapsed;

            if (ChaussuresViewSource.CurrentItem != null)
            {
                Personne p = (Personne)PersonnesViewSource.CurrentItem;
                Chaussure c = (Chaussure)ChaussuresViewSource.CurrentItem;

                PersonneChaussure pc = new PersonneChaussure();

                pc.Id_personneChaussure = -1;
                pc.Id_personne = p.Id_Personne;

                pc.Id_chaussure = c.Id_chaussure;
                pc.Qte_stock = 1;
                pc.Prix_vente = c.Prix;
               //c.Chaussure = c
               
                p.PersonneChaussures.Add(pc);

            }




        }

        private void cancelSelectedChaussure(object obj)
        {
            VisibilityListSelectChaussure = Visibility.Collapsed;
        }

        private void KillChild(object obj)
        {
            Personne p = (Personne)PersonnesViewSource.CurrentItem;
            if (obj is PersonneChaussure)
            {
                PersonneChaussure pc = (PersonneChaussure)obj;

                string messageBoxText = "Etes-vous certain de vouloir supprimer cette chaussure?";
                string caption = "Vous etes sur le point de détruire une chaussure";
                MessageBoxButton button = MessageBoxButton.OKCancel;
                MessageBoxImage icon = MessageBoxImage.Warning;

                MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);

                if (result == MessageBoxResult.OK)
                {
                    if (ActionModeActuel == ACTIONMODE.EDIT)
                    {
                        if (pc.Id_personneChaussure > 0)
                        {
                            p.ListpersonneChaussureToDelete.Add(pc);
                        }
                    }
                    p.PersonneChaussures.Remove(pc);
                }
                else
                {
                    MessageBox.Show("erreur lors de la destruction");
                }
            }
            else
            {
                MessageBox.Show("Vous devez sélectionner une chaussure");
            }
        }

        private void AddChaussure(object obj)
        {
            VisibilityListSelectChaussure = Visibility.Visible;
        }

        private void CancelPersonne(object obj)
        {
            Personne personne = (Personne)PersonnesViewSource.CurrentItem;

            if (ActionModeActuel == ACTIONMODE.ADD)
            {
                bool r = ds.Personnes.Remove(personne);
                PersonnesViewSource.Refresh();
            }
            else
            {
                ds.Reload(personne);

                PersonnesViewSource.Refresh();
                PersonnesViewSource.MoveCurrentTo(null);
                PersonnesViewSource.MoveCurrentTo(personne);
            }
            ActionModeActuel = ACTIONMODE.DISPLAY;
        }

        private void EditPersonne(object obj)
        {
            ActionModeActuel = ACTIONMODE.EDIT;
        }

        private void ModifParCode(object obj)
        {
            if (PersonnesViewSource.CurrentItem != null)
            {
                Personne personne = (Personne)PersonnesViewSource.CurrentItem;
                personne.Age += 11;

                //pcq pas de notification dans la classe Personne
                // ceci refresh la liste
                PersonnesViewSource.Refresh();
                // ceci refresh le courant en haut 
                PersonnesViewSource.MoveCurrentTo(null);
                PersonnesViewSource.MoveCurrentTo(personne);
            }

        }

        private void AddPersonne(object obj)
        {
            ActionModeActuel = ACTIONMODE.ADD;


            Personne personne = new Personne("new Personne");
            // impossible d'ajouter dans une vue
            ActionModeActuel = ACTIONMODE.ADD;

            ds.Personnes.Add(personne);
            personne.Date_CreationBidon = DateTime.Now;
            PersonnesViewSource.MoveCurrentTo(personne);

        }

        private void DeletePersonne(object obj)
        {
            if (PersonnesViewSource.CurrentItem != null)
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
                    Personne personne = (Personne)PersonnesViewSource.CurrentItem;
                    ds.Delete(personne);
                    // ds.Delete(personne);  // on ne peut pas faire de remove sur une vue

                }
            }
            else
            {
                MessageBox.Show("Vous devez sélectionner une personne");
            }


        }


        private void SavePersonne(object obj)
        {// en edit et add
            Personne personne = (Personne)PersonnesViewSource.CurrentItem;
            if (personne != null)
            { // validations ici pour le moment
                if (personne.NomFamille.Length < 3)
                {
                    MessageBox.Show("Entrez un nom valide");
                    return;
                }
                ds.Save(personne);

                // maj des controles autres que liste si nous modifions
                // une valeur par code et non par saisie à l'écran
                // simulation de clic sur un autre et revient dessus
                PersonnesViewSource.MoveCurrentTo(null);
                PersonnesViewSource.MoveCurrentTo(personne);
                // maj de des listes ex lisxbox
                PersonnesViewSource.Refresh();
            }

            ActionModeActuel = ACTIONMODE.DISPLAY;

        }






        #region navigation

        private void NextPersonne(object obj)
        {
            PersonnesViewSource.MoveCurrentToNext();

            if (PersonnesViewSource.IsCurrentAfterLast)
            {
                PersonnesViewSource.MoveCurrentToPrevious();
            }




        }
        private void LastPersonne(object obj)
        {

            PersonnesViewSource.MoveCurrentToLast();

        }
        private void PrevPersonne(object obj)
        {
            PersonnesViewSource.MoveCurrentToPrevious();
            if (PersonnesViewSource.IsCurrentBeforeFirst)
            {
                PersonnesViewSource.MoveCurrentToFirst();
            }



        }
        private void FirstPersonne(object obj)
        {
            PersonnesViewSource.MoveCurrentToFirst();


        }

        #endregion

    }
}
