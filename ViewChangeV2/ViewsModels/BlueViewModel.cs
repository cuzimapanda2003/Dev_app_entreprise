using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewChangeV2.Models;

namespace ViewChangeV2.ViewsModels
{
    public class BlueViewModel : BaseViewModel
    {
        public Animal Animal { get; set; } = new Animal() {Name="chat", Age=10 };

        public List<Animal> Animals { get; set; } = new List<Animal>();
        public BlueViewModel() {

            Animals.Add(Animal);
            Animal ani = new Animal();
            ani.Name = "chien";
            ani.Age = 10;
            Animals.Add(ani);
        }



    }
}
