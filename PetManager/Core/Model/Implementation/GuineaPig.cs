using PetManager.Core.Model.Abstraction;
using PetManager.Core.Model.Type;

namespace PetManager.Core.Model.Implementation
{
    public class GuineaPig : AbstractAnimal
    {
        public GuineaPig(string name, ColorEnum color) : base(SpecieEnum.Cat, name, color) { }

        protected override CaringSetup InitializeDefaultCaringSetup()
        {
            // Eating constraint
            Dictionary<AnimalNeedEnum, int> eatingLimits = new Dictionary<AnimalNeedEnum, int>();
            eatingLimits.Add(AnimalNeedEnum.Hydrating, 7);

            CaringConstraint eatingConstraint = new CaringConstraint(AnimalNeedEnum.Eating, 1, 4, eatingLimits);

            // Hydrating constraint
            Dictionary<AnimalNeedEnum, int> hydratingLimits = new Dictionary<AnimalNeedEnum, int>();
            CaringConstraint hydratingConstraint = new CaringConstraint(AnimalNeedEnum.Hydrating, 1, 4, hydratingLimits);

            // Resting constraint
            Dictionary<AnimalNeedEnum, int> restingLimits = new Dictionary<AnimalNeedEnum, int>();
            restingLimits.Add(AnimalNeedEnum.Eating, 7);
            restingLimits.Add(AnimalNeedEnum.Hydrating, 6);

            CaringConstraint restingConstraint = new CaringConstraint(AnimalNeedEnum.Resting, 1, 2, restingLimits);

            // PLaying constraint
            Dictionary<AnimalNeedEnum, int> playingLimits = new Dictionary<AnimalNeedEnum, int>();
            playingLimits.Add(AnimalNeedEnum.Eating, 5);
            playingLimits.Add(AnimalNeedEnum.Hydrating, 5);

            CaringConstraint playingConstraint = new CaringConstraint(AnimalNeedEnum.Playing, 2, 4, playingLimits);

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
