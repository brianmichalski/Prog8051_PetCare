﻿using PetManager.Core.Model.Abstraction;
using PetManager.Core.Model.Type;

namespace PetManager.Core.Model.Implementation
{
    public class Cat : AbstractAnimal
    {
        public Cat(string name) : base(SpecieEnum.Cat, name) { }

        protected override CaringSetup InitializeDefaultCaringSetup()
        {
            // Eating constraint
            Dictionary<AnimalNeedEnum, int> eatingLimits = new Dictionary<AnimalNeedEnum, int>();
            eatingLimits.Add(AnimalNeedEnum.Hydrating, 7);

            CaringConstraint eatingConstraint = new CaringConstraint(AnimalNeedEnum.Eating, 2, 8, eatingLimits);

            // Hydrating constraint
            Dictionary<AnimalNeedEnum, int> hydratingLimits = new Dictionary<AnimalNeedEnum, int>();
            CaringConstraint hydratingConstraint = new CaringConstraint(AnimalNeedEnum.Hydrating, 1, 4, hydratingLimits);

            // Resting constraint
            Dictionary<AnimalNeedEnum, int> restingLimits = new Dictionary<AnimalNeedEnum, int>();
            restingLimits.Add(AnimalNeedEnum.Eating, 9);
            restingLimits.Add(AnimalNeedEnum.Hydrating, 8);

            CaringConstraint restingConstraint = new CaringConstraint(AnimalNeedEnum.Resting, 3, 5, restingLimits);

            // PLaying constraint
            Dictionary<AnimalNeedEnum, int> playingLimits = new Dictionary<AnimalNeedEnum, int>();
            playingLimits.Add(AnimalNeedEnum.Eating, 8);
            playingLimits.Add(AnimalNeedEnum.Hydrating, 7);

            CaringConstraint playingConstraint = new CaringConstraint(AnimalNeedEnum.Playing, 3, 5, playingLimits);

            Dictionary<AnimalNeedEnum, CaringConstraint> caringConstraints = new Dictionary<AnimalNeedEnum, CaringConstraint>();
            caringConstraints.Add(AnimalNeedEnum.Eating, eatingConstraint);
            caringConstraints.Add(AnimalNeedEnum.Hydrating, hydratingConstraint);
            caringConstraints.Add(AnimalNeedEnum.Playing, restingConstraint);
            caringConstraints.Add(AnimalNeedEnum.Resting, playingConstraint);

            return new CaringSetup(this, 
                new List<FoodGroupEnum>([FoodGroupEnum.Meat, FoodGroupEnum.Kibble]),
                caringConstraints);
        }
    }
}
