using PetManager.Core.Model.Type;

namespace PetManager.Core.Model
{
    public class CaringConstraint
    {
        AnimalNeedEnum AnimalNeed;
        public int MinimumIntervalInHours { get; set; }
        public int MaximumIntervalInHours { get; set; }
        public Dictionary<AnimalNeedEnum, int> LimitsForOtherNeeds { get; set; }

        public CaringConstraint(AnimalNeedEnum animalNeed, int minimumIntervalInHours, int maximumIntervalInHours, 
            Dictionary<AnimalNeedEnum, int> limitsForOtherNeeds)
        {
            this.AnimalNeed = animalNeed;
            this.MinimumIntervalInHours = minimumIntervalInHours;
            this.MaximumIntervalInHours = maximumIntervalInHours;
            this.LimitsForOtherNeeds = limitsForOtherNeeds;
        }
    }
}
