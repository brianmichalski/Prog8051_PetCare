using PetManager.Core.Model;
using PetManager.Core.Model.Type;

namespace PetManager.Core.Model.Abstraction
{
    public interface IAnimal
    {
        SpecieEnum Specie { get; }

        string Name { get; set; }

        CaringConstraint CaringConstraint { get; }

        DateTime LastTimeResting { get; set; }
        DateTime LastTimeHydrating { get; set; }
        DateTime LastTimeEating { get; set; }
        DateTime LastTimePlaying { get; set; }

        public void Rest();

        public void Hydrate();

        public void Eat();

        public void Play();

        public int CalculateRestingStressLevel(DateTime checkingTime);

        public int CalculateEatingStressLevel(DateTime checkingTime);

        public int CalculateHydratingStressLevel(DateTime checkingTime);

        public int CalculatePlayingStressLevel(DateTime checkingTime);
    }
}
