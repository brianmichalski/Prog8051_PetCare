using PetManager.Core.Model.Abstraction;
using PetManager.Core.Model.Type;

namespace PetManager.Core.Model
{
    public class CaringConstraint
    {
        public IAnimal Animal { get; }
        public List<FoodGroupEnum> FoodTypes { get; }

        public int EatingIntervalInHours;

        public int HydratingIntervalInHours;

        public int RestingIntervalInHours;

        public int PlayingIntervalInHours;

        public CaringConstraint(IAnimal animal, List<FoodGroupEnum> foodTypes) 
        { 
            this.Animal = animal;
            this.FoodTypes = foodTypes;
        }
    }
}
