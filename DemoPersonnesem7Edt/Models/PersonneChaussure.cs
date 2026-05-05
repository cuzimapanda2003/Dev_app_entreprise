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
    public class PersonneChaussure //: INotifyPropertyChanged
    {
        public Chaussure Chaussure { get; set; }
        public int Id_personneChaussure { get; set; }
        public int Id_personne { get; set; }
        public int Id_chaussure { get; set; }
        public int Qte_stock { get; set; }
        public decimal Prix_vente { get; set; }
    }
}

