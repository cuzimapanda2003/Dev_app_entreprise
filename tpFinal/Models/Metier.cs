using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tpFinal.Models
{
    public class Metier
    {
        public int id {  get; set; }
        public string name { get; set; }
        public int bonus_combat { get; set; }

        public Metier() 
        {
            id = -1;
            name = "place holder";
            bonus_combat = -1;
        }
    }
}
