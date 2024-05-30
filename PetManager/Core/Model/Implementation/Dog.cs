using PetManager.Core.Model.Abstraction;
using PetManager.Core.Model.Type;

namespace PetManager.Core.Model.Implementation
{
    public class Dog : AbstractAnimal
    {
        public Dog(string name) : base(SpecieEnum.Dog, name) { }
        protected override CaringConstraint InitializeDefaultCaringContraint()
        {
            return new CaringConstraint
            {
                Animal = this,
                FoodTypes = new List<FoodGroupEnum>([FoodGroupEnum.Meat, FoodGroupEnum.Kibble]),
                EatingIntervalInHours = 12,
                HydratingIntervalInHours = 3,
                RestingIntervalInHours = 4,
                PlayingIntervalInHours = 6
            };
        }
    }
}
