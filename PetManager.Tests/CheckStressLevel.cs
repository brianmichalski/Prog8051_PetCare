using PetManager.Core.Model;
using PetManager.Core.Model.Exception;
using PetManager.Core.Model.Type;
using PetManager.Service;

namespace PetManager.Tests;

public class CheckStressLevelTests
{
    PetService PetService;

    [SetUp]
    public void Setup()
    {
        this.PetService = PetService.GetInstance();
    }

    [Test]
    [TestCase(AnimalNeedEnum.Eating, SpecieEnum.Dog, "Snoopy")]
    public void StressLevel_AfterCreatingAnimal_Pass(AnimalNeedEnum testingNeed, SpecieEnum specie, string name)
    {
        var animal = this.PetService.CreateAnimal(specie, name);

        Assert.That(CaringStressCalculator.CalculateNeedStressLevel(
            animal, testingNeed, DateTime.Now), Is.EqualTo(0));
    }

    [Test]
    [TestCase(AnimalNeedEnum.Eating, SpecieEnum.Cat, "Meow")]
    public void StressLevel_AfterFirstCaring_Pass(AnimalNeedEnum testingNeed, SpecieEnum specie, string name)
    {
        var animal = this.PetService.CreateAnimal(specie, name);

        int minimumIntervalForCaring = animal.CaringSetup.CaringConstraints[testingNeed].MinimumIntervalInHours;

        DateTime caringTime = DateTime.Now.AddHours(minimumIntervalForCaring);
        switch (testingNeed)
        {
            case AnimalNeedEnum.Eating:
                animal = this.PetService.Feed(animal, caringTime);
                break;
            case AnimalNeedEnum.Hydrating:
                animal = this.PetService.GiveWater(animal, caringTime);
                break;
            case AnimalNeedEnum.Resting:
                animal = this.PetService.PutToSleep(animal, caringTime);
                break;
            case AnimalNeedEnum.Playing:
                animal = this.PetService.PlayWith(animal, caringTime);
                break;
        }

        Assert.That(CaringStressCalculator.CalculateNeedStressLevel(
            animal, testingNeed, DateTime.Now), Is.EqualTo(0));
    }

    [Test]
    [TestCase(AnimalNeedEnum.Eating, SpecieEnum.Cat, "Meow")]
    public void StressLevel_RangeCovering_Pass(AnimalNeedEnum testingNeed, SpecieEnum specie, string name)
    {
        var animal = this.PetService.CreateAnimal(specie, name);

        CaringConstraint caringConstraint = animal.CaringSetup.CaringConstraints[testingNeed];

        DateTime firstCaringTime = DateTime.Now.AddHours(caringConstraint.MinimumIntervalInHours);

        switch (testingNeed)
        {
            case AnimalNeedEnum.Eating:
                animal = this.PetService.Feed(animal, firstCaringTime);
                break;
            case AnimalNeedEnum.Hydrating:
                animal = this.PetService.GiveWater(animal, firstCaringTime);
                break;
            case AnimalNeedEnum.Resting:
                animal = this.PetService.PutToSleep(animal, firstCaringTime);
                break;
            case AnimalNeedEnum.Playing:
                animal = this.PetService.PlayWith(animal, firstCaringTime);
                break;
        }

        for (int hoursInFuture = caringConstraint.MinimumIntervalInHours;
             hoursInFuture <= caringConstraint.MaximumIntervalInHours * 2; hoursInFuture++)
        {
            double stressLevel = CaringStressCalculator.CalculateNeedStressLevel(
                animal, testingNeed, firstCaringTime.AddHours(hoursInFuture));

            Assert.That(stressLevel, Is.GreaterThan(0));
            Assert.That(stressLevel, Is.LessThanOrEqualTo(10));
        }
    }

    [TestCase(AnimalNeedEnum.Eating, SpecieEnum.Cat, "Meow")]
    public void StressLevel_TryToCareBeforeNeeding_Fail(AnimalNeedEnum testingNeed, SpecieEnum specie, string name)
    {
        var animal = this.PetService.CreateAnimal(specie, name);

        int minimumIntervalForCaring = animal.CaringSetup.CaringConstraints[testingNeed].MinimumIntervalInHours;

        DateTime caringTime = DateTime.Now.AddHours(minimumIntervalForCaring - 1);
        TestDelegate callback = () => { };
        switch (testingNeed)
        {
            case AnimalNeedEnum.Eating:
                callback = () => this.PetService.Feed(animal, caringTime);
                break;
            case AnimalNeedEnum.Hydrating:
                callback = () => this.PetService.GiveWater(animal, caringTime);
                break;
            case AnimalNeedEnum.Resting:
                callback = () => this.PetService.PutToSleep(animal, caringTime);
                break;
            case AnimalNeedEnum.Playing:
                callback = () => this.PetService.PlayWith(animal, caringTime);
                break;
        }

        Assert.Catch<MinimumStressLevelException>(callback);
    }
}