﻿using PetManager.Core.Model;
using PetManager.Core.Model.Type;
using System.ComponentModel;

namespace PetManager.Core.Model.Abstraction
{
    public abstract class AbstractAnimal : IAnimal
    {
        public SpecieEnum Specie { get; }

        public string Name { get; set; }

        public CaringConstraint CaringConstraint { get; set; }

        public DateTime LastTimeResting { get; set; } = DateTime.MinValue;

        [DefaultValue(null)]
        public DateTime LastTimeHydrating { get; set; } = DateTime.MinValue;

        [DefaultValue(null)]
        public DateTime LastTimeEating { get; set; } = DateTime.MinValue;

        [DefaultValue(null)]
        public DateTime LastTimePlaying { get; set; } = DateTime.MinValue;

        protected AbstractAnimal(SpecieEnum specie,
                                 string name)
        {
            this.Specie = specie;
            this.Name = name;
            this.CaringConstraint = InitializeDefaultCaringContraint();
        }
        protected abstract CaringConstraint InitializeDefaultCaringContraint();
        public void Rest() { 
            this.LastTimeResting = DateTime.Now;
        }
        public void Hydrate()
        {
            this.LastTimeHydrating = DateTime.Now;
        }
        public void Eat()
        {
            this.LastTimeEating = DateTime.Now;
        }
        public void Play()
        {
            this.LastTimePlaying = DateTime.Now;
        }
        public int CalculateRestingStressLevel(DateTime checkingTime)
        {
            return this.CalculateStressLevel(
                this.CaringConstraint.RestingIntervalInHours,
                this.LastTimeResting,
                checkingTime);
        }
        public int CalculateEatingStressLevel(DateTime checkingTime)
        {
            return this.CalculateStressLevel(
                this.CaringConstraint.EatingIntervalInHours,
                this.LastTimeEating,
                checkingTime);
        }

        public int CalculateHydratingStressLevel(DateTime checkingTime)
        {
            return this.CalculateStressLevel(
                this.CaringConstraint.HydratingIntervalInHours,
                this.LastTimeHydrating,
                checkingTime);
        }

        public int CalculatePlayingStressLevel(DateTime checkingTime)
        {
            return this.CalculateStressLevel(
                this.CaringConstraint.PlayingIntervalInHours,
                this.LastTimePlaying,
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
