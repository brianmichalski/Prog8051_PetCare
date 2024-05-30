using PetManager.Core.Model.Abstraction;
using PetManager.Core.Model.Type;

namespace PetManager.Core.Model
{
    public class CaringConstraint
    {
        public IAnimal Animal { get; set; }
        public List<FoodGroupEnum> FoodTypes { get; set; }

        public int EatingIntervalInHours;

        public int HydratingIntervalInHours;

        public int RestingIntervalInHours;

        public int PlayingIntervalInHours;

    }
}
