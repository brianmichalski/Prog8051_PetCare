
using PetManager.Core.Model.Abstraction;
using PetManager.Core.Model.Exception;
using PetManager.Core.Model.Type;

namespace PetManager.Core.Model { 

    public class CaringStressCalculator
    {

        public static double CalculateNeedStressLevel(IAnimal animal, AnimalNeedEnum need, DateTime checkingTime)
        {
            return CalculateStressLevel(
                animal.CaringSetup.CaringConstraints[need],
                animal.LastTimeCaringMap[need],
                checkingTime);
        }
        private static double CalculateStressLevel(CaringConstraint caringConstraint,
            DateTime lastCaringTime, DateTime stressCheckingTime)
        {
            const int MIN_STRESS_LEVEL = 0;
            const int MAX_STRESS_LEVEL = 10;

            TimeSpan timeSpan = stressCheckingTime.Subtract(lastCaringTime);
            if (timeSpan.Hours < caringConstraint.MinimumIntervalInHours)
            {
                return MIN_STRESS_LEVEL;
            }

            double delayRatio = (double) timeSpan.Hours / caringConstraint.MaximumIntervalInHours;

            return (double) MAX_STRESS_LEVEL * (delayRatio < 1 ? delayRatio : 1);
        }
        private static bool WasCaringMinimumIntervalReached(IAnimal animal, AnimalNeedEnum need, DateTime checkingTime)
        {
            CaringConstraint caringConstraint = animal.CaringSetup.CaringConstraints[need];
            TimeSpan caringTimeGap = checkingTime.Subtract(animal.LastTimeCaringMap[need]);
            return (caringTimeGap.Hours >= caringConstraint.MinimumIntervalInHours);
        }
        public static void CheckCaringMinimumInterval(IAnimal animal, AnimalNeedEnum need, DateTime checkingTime)
        {
            if (!CaringStressCalculator.WasCaringMinimumIntervalReached(animal, need, checkingTime))
            {
                throw new MinimumStressLevelException(
                    string.Format("{0} is not in need for {1} yet.", animal.Name, need.ToString()));
            }
        }

        public static void CheckOtherNeedsLimits(IAnimal animal, AnimalNeedEnum need, DateTime checkingTime)
        {
            CaringConstraint caringConstraint = animal.CaringSetup.CaringConstraints[need];
            foreach (KeyValuePair<AnimalNeedEnum, int> otherNeedStressLimit in caringConstraint.StressLevelLimitsForOtherNeeds)
            {
                double stressLevelForOtherNeed = CalculateNeedStressLevel(animal, otherNeedStressLimit.Key, checkingTime);
                if (stressLevelForOtherNeed >= otherNeedStressLimit.Value)
                {
                    throw new MaximumStressLevelException(
                        string.Format("{0} can not be performed because {1} is over the stress limit.",
                            need.ToString(), otherNeedStressLimit.Key.ToString()));
                }
            }
        }
    }
}
