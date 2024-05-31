using PetManager.Core.Model.Abstraction;
using PetManager.Core.Model.Type;

namespace PetManager.Core.Model.Implementation
{
    public class GuineaPig : AbstractAnimal
    {
        public GuineaPig(string name) : base(SpecieEnum.GuineaPig, name) { }

        protected override CaringConstraint InitializeDefaultCaringContraint()
        {
            return new CaringConstraint(this,
                new List<FoodGroupEnum>([FoodGroupEnum.Vegetable]))
            {
                EatingIntervalInHours = 6,
                HydratingIntervalInHours = 2,
                RestingIntervalInHours = 3,
                PlayingIntervalInHours = 2
            };
        }
    }
}
