using PetManager.Core.Model.Abstraction;
using PetManager.Core.Model.Type;

namespace PetManager.Core.Model.Implementation
{
    public class Turtle : AbstractAnimal
    {
        public Turtle(string name, ColorEnum color) : base(SpecieEnum.Cat, name, color) { }

        protected override CaringSetup InitializeDefaultCaringSetup()
        {
            // Eating constraint
            Dictionary<AnimalNeedEnum, int> eatingLimits = new Dictionary<AnimalNeedEnum, int>();
            eatingLimits.Add(AnimalNeedEnum.Hydrating, 9);

            CaringConstraint eatingConstraint = new CaringConstraint(AnimalNeedEnum.Eating, 2, 8, eatingLimits);

            // Hydrating constraint
            Dictionary<AnimalNeedEnum, int> hydratingLimits = new Dictionary<AnimalNeedEnum, int>();
            CaringConstraint hydratingConstraint = new CaringConstraint(AnimalNeedEnum.Hydrating, 3, 8, hydratingLimits);

            // Resting constraint
            Dictionary<AnimalNeedEnum, int> restingLimits = new Dictionary<AnimalNeedEnum, int>();
            restingLimits.Add(AnimalNeedEnum.Eating, 9);
            restingLimits.Add(AnimalNeedEnum.Hydrating, 8);

            CaringConstraint restingConstraint = new CaringConstraint(AnimalNeedEnum.Resting, 1, 3, restingLimits);

            // PLaying constraint
            Dictionary<AnimalNeedEnum, int> playingLimits = new Dictionary<AnimalNeedEnum, int>();
            playingLimits.Add(AnimalNeedEnum.Eating, 8);
            playingLimits.Add(AnimalNeedEnum.Hydrating, 7);

            CaringConstraint playingConstraint = new CaringConstraint(AnimalNeedEnum.Playing, 1, 4, playingLimits);

            Dictionary<AnimalNeedEnum, CaringConstraint> caringConstraints = new Dictionary<AnimalNeedEnum, CaringConstraint>();
            caringConstraints.Add(AnimalNeedEnum.Eating, eatingConstraint);
            caringConstraints.Add(AnimalNeedEnum.Hydrating, hydratingConstraint);
            caringConstraints.Add(AnimalNeedEnum.Playing, restingConstraint);
            caringConstraints.Add(AnimalNeedEnum.Resting, playingConstraint);

            return new CaringSetup(this,
                new List<FoodGroupEnum>([FoodGroupEnum.Vegetable]), caringConstraints);
        }
    }
}
