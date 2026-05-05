using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WpfPersonne.Models
{// veuillez a standardiser où on place les initialisation
   // constructeur ou propriétés?
   public class Personne //: INotifyPropertyChanged
    {// lien enfants
        public virtual ObservableCollection<PersonneChaussure> PersonneChaussures
        {
            get; set;
        } = new ObservableCollection<PersonneChaussure>();
        public List<PersonneChaussure> ListpersonneChaussureToDelete { get; set; } = new List<PersonneChaussure>();

        static int seqnum = 0;
        // ajout id personne pour la table clef primaire
        public int Id_Personne { get; set; }
        // ajout date decimal et by pour checkbox
        public DateTime Date_CreationBidon { get; set; }
        public decimal Total_Ventes { get; set; }
       

        private byte actif = 1;
        public byte Actif
        {
            get
            { return actif; }
            set
            { actif = value; }
        }
        public string NomFamille { get; set; }
       public string Prenom { get; set; }
        //private int age;
        public int Age { get; set; }
        // ne sera pas mis à jour
        public string NomComplet { get => Prenom + " " + NomFamille; }
       
        
        // ajouter pour faire des liens 1-1 (classe Province et Profession)
        
        // On affichera le nom dans un combobox 
        public string Code_Province { get; set; }="QC";
       // public virtual Province Province { get; set; }
        
        // on affichera la description dans un texblock
        public int Id_Profession { get; set; } = 1;
       // public virtual Profession Profession { get; set; }


        public Personne(string Prenom = "inconnu", string NomFamille = "inconnu", int Age = 1)
        {
            this.Id_Personne = -1;
            this.Prenom = Prenom;
            this.NomFamille = NomFamille;
            this.Age = Age;
            //***ici
            this.Date_CreationBidon = DateTime.Today;
            Total_Ventes = 15.5M;
            // this.Date_CreationBidon = DateTime.Today.AddMonths(1);

        }
        
        
    }
}

