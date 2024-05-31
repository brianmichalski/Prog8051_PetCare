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

        public CaringSetup CaringSetup { get; set; }

        public Dictionary<AnimalNeedEnum, DateTime> LastTimeCaringMap;

        protected AbstractAnimal(SpecieEnum specie, string name)
        {
            this.Specie = specie;
            this.Name = name;
            this.CaringSetup = InitializeDefaultCaringSetup();
            this.LastTimeCaringMap = new Dictionary<AnimalNeedEnum, DateTime>();
            foreach (AnimalNeedEnum animalNeed in Enum.GetValues(typeof(AnimalNeedEnum)))
            {
                this.LastTimeCaringMap.Add(animalNeed, DateTime.MinValue);
            }
        }
        protected abstract CaringSetup InitializeDefaultCaringSetup();
        public void Rest(DateTime currentTime) {
            this.LastTimeCaringMap[AnimalNeedEnum.Resting] = currentTime;
        }
        public void Hydrate(DateTime currentTime)
        {
            this.LastTimeCaringMap[AnimalNeedEnum.Hydrating] = currentTime;
        }
        public void Eat(DateTime currentTime)
        {
            this.LastTimeCaringMap[AnimalNeedEnum.Eating] = currentTime;
        }
        public void Play(DateTime currentTime)
        {
            this.LastTimeCaringMap[AnimalNeedEnum.Resting] = currentTime;
        }
        public int CalculateNeedStressLevel(AnimalNeedEnum need, DateTime checkingTime)
        {
            return this.CalculateStressLevel(
                this.CaringSetup.CaringConstraints[need].MaximumIntervalInHours,
                this.LastTimeCaringMap[need],
                checkingTime);
        }

        private int CalculateStressLevel(int maximumIntervalInHours, 
            DateTime lastCaringTime, DateTime stressCheckingTime)
        {
            const int MAX_STRESS = 10;
            if (lastCaringTime.Equals(DateTime.MinValue))
            {
                return MAX_STRESS;
            }

            TimeSpan timeSpan = stressCheckingTime.Subtract(lastCaringTime);

            double delayRatio =  (double) timeSpan.Hours / maximumIntervalInHours;

            return MAX_STRESS * (delayRatio < 1 ? (int) Math.Ceiling(delayRatio) : 1);
        }
    }
}
