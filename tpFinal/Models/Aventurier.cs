using System;
using System.Collections.ObjectModel;

namespace tpFinal.Models
{
    public class Aventurier
    {
        public int id { get; set; }
        public string nom { get; set; }
        public int niveau { get; set; }
        public int id_metier { get; set; }
        public decimal solde_or { get; set; }
        public DateTime DateInscription { get; set; }
        public ObservableCollection<AventurierEquipement> AventurierEquipements { get; set; }
        public ObservableCollection<AventurierEquipement> ListEquipementsToDelete { get; set; }

        public Aventurier()
        {
            id = -1;
            nom = "name holder";
            niveau = -1;
            id_metier = -1;
            solde_or = -1.0m;
            DateInscription = DateTime.Now;
            AventurierEquipements = new ObservableCollection<AventurierEquipement>();
            ListEquipementsToDelete = new ObservableCollection<AventurierEquipement>();
        }
    }
}