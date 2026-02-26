using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewChangeV2.Commands;
using ViewChangeV2.Models;

namespace ViewChangeV2.ViewsModels
{
    public class BlueViewModel : BaseViewModel
    {
        private Animal selected;

        public Animal Animal { get; set; } = new Animal() {Name="chat", Age=10 };

        public ObservableCollection<Animal> Animals { get; set; } = new ObservableCollection<Animal>();
        public BlueViewModel() {

            Animals.Add(Animal);
            Animal ani = new Animal();
            ani.Name = "chien";
            ani.Age = 10;
            Animals.Add(ani);
            Selected = Animals.FirstOrDefault();
            AddCommand = new RelayCommand(addAnimal, null);
            DeleteCommand = new RelayCommand(deleteAnimal, null);
            Selected = Animals.LastOrDefault();
            
        }

        private void addAnimal(object obj)
        {
            Animals.Add(new Animal() { Name = "new" });
            Selected = Animals.LastOrDefault();
        }

        private void deleteAnimal(object obj)
        {

        }

        public Animal Selected
        {
            get => selected;
            set {
                selected = value; 
                OnPropertyChanged();
            }
        }

        public RelayCommand AddCommand {  get; set; }
        public RelayCommand DeleteCommand { get; set; }


    }
}
