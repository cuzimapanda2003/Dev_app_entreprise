using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tpFinal.Models
{
    public class Equipement
    {
        public int id { get; set; }
        public string description { get; set; }
        public int code_type_objet { get; set; }
        public int quantite {  get; set; }
        public decimal prix_unitaire { get; set; }
        public TypeObjet TypeObjet { get; set; }

    }
}
