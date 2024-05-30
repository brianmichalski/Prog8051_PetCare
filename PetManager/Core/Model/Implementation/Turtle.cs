using PetManager.Core.Model.Abstraction;
using PetManager.Core.Model.Type;

namespace PetManager.Core.Model.Implementation
{
    public class Turtle : AbstractAnimal
    {
        public Turtle(string name) : base(SpecieEnum.Turtle, name) { }

        protected override CaringConstraint InitializeDefaultCaringContraint()
        {
            return new CaringConstraint
            {
                Animal = this,
                FoodTypes = new List<FoodGroupEnum>([FoodGroupEnum.Vegetable]),
                EatingIntervalInHours = 12,
                HydratingIntervalInHours = 3,
                RestingIntervalInHours = 4,
                PlayingIntervalInHours = 6
            };
        }
    }
}
