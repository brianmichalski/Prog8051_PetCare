using PetManager.Core.Model.Abstraction;
using PetManager.Core.Model.Type;

namespace PetManager.Core.Model.Implementation
{
    public class Cat : AbstractAnimal
    {
        public Cat(string name) : base(SpecieEnum.Cat, name) { }

        protected override CaringConstraint InitializeDefaultCaringContraint()
        {
            return new CaringConstraint(this, 
                new List<FoodGroupEnum>([FoodGroupEnum.Meat, FoodGroupEnum.Kibble]))
            {
                EatingIntervalInHours = 8,
                HydratingIntervalInHours = 2,
                RestingIntervalInHours = 3,
                PlayingIntervalInHours = 4
            };
        }
    }
}
