using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tpFinal.Models
{
    public class AventurierEquipement
    {
        public int id { get; set; }
        public int id_aventurier { get; set; }
        public int id_equipement { get; set; }
        public int quantite_totale { get; set; }
        public int quantite_active { get; set; }
        public decimal prix_achat { get; set; }
        public decimal Depense => prix_achat * quantite_active;
        public Equipement Equipement { get; set; }
        public AventurierEquipement()
        {
            id = -1;
            id_aventurier = -1;
            id_equipement = -1;
            quantite_totale = 1;
            quantite_active = 0;
            prix_achat = 0;
        }
    }
}