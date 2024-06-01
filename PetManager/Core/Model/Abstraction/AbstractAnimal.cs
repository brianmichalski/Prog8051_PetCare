using PetManager.Core.Model;
using PetManager.Core.Model.Exception;
using PetManager.Core.Model.Type;
using System.ComponentModel;

namespace PetManager.Core.Model.Abstraction
{
    public abstract class AbstractAnimal : IAnimal
    {
        public SpecieEnum Specie { get; }

        public string Name { get; set; }
        public ColorEnum Color { get; set; }

        public CaringSetup CaringSetup { get; set; }

        public IDictionary<AnimalNeedEnum, DateTime> LastTimeCaringMap { get; }

        protected AbstractAnimal(SpecieEnum specie, string name, ColorEnum color)
        {
            this.Specie = specie;
            this.Name = name;
            this.Color = color;
            this.CaringSetup = InitializeDefaultCaringSetup();
            this.LastTimeCaringMap = new Dictionary<AnimalNeedEnum, DateTime>();

            DateTime nowTime = DateTime.Now;
            foreach (AnimalNeedEnum animalNeed in Enum.GetValues(typeof(AnimalNeedEnum)))
            {
                this.LastTimeCaringMap.Add(animalNeed, nowTime);
            }
            Color = color;
        }
        protected abstract CaringSetup InitializeDefaultCaringSetup();
        private void PerformCaring(AnimalNeedEnum need, DateTime performingTime)
        {
            CaringStressCalculator.CheckCaringMinimumInterval(this, need, performingTime);

            CaringStressCalculator.CheckOtherNeedsLimits(this, need, performingTime);

            this.LastTimeCaringMap[need] = performingTime;
        }
        public void Rest(DateTime currentTime)
        {
            this.PerformCaring(AnimalNeedEnum.Resting, currentTime);
        }
        public void Hydrate(DateTime currentTime)
        {
            this.PerformCaring(AnimalNeedEnum.Hydrating, currentTime);
        }
        public void Eat(DateTime currentTime)
        {
            this.PerformCaring(AnimalNeedEnum.Eating, currentTime);
        }
        public void Play(DateTime currentTime)
        {
            this.PerformCaring(AnimalNeedEnum.Playing, currentTime);
        }
    }
}
