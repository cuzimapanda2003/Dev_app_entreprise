using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfPersonne.Models
{
    public class Chaussure
    {
        public int Id_chaussure { get; set; }
        public decimal Taille { get; set; } = 7m;
        public string Code_genreChaussure { get; set; }
        public string Code_typeChaussure { get; set; } = "non défini";
        public decimal Prix { get; set; } = 0m;
    }

}
