using PetManager.Core.Model.Abstraction;
using PetManager.Core.Model.Type;

namespace PetManager.Core.Model
{
    public class CaringSetup
    {
        public IAnimal Animal { get; }
        public List<FoodGroupEnum> FoodTypes { get; }

        public Dictionary<AnimalNeedEnum, CaringConstraint> CaringConstraints { get; }

        public CaringSetup(IAnimal animal, List<FoodGroupEnum> foodTypes, 
            Dictionary<AnimalNeedEnum, CaringConstraint> caringConstraints) 
        {
            this.Animal = animal;
            this.FoodTypes = foodTypes;
            this.CaringConstraints = caringConstraints;
        }
    }
}
