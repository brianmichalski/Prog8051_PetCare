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
    public void EatingStressLevel_BeforeFirstFeeding_Pass(AnimalNeedEnum need, SpecieEnum specie, string name)
    {
        var animal = this.PetService.CreateAnimal(specie, name);

        Assert.That(animal.CalculateNeedStressLevel(need, DateTime.Now), Is.EqualTo(10));
    }

    [Test]
    [TestCase(AnimalNeedEnum.Eating, SpecieEnum.Cat, "Meow")]
    public void EatingStressLevel_AfterFirstFeeding_Pass(AnimalNeedEnum need, SpecieEnum specie, string name)
    {
        var animal = this.PetService.CreateAnimal(specie, name);
        animal = this.PetService.Feed(animal, DateTime.Now);

        Assert.That(animal.CalculateNeedStressLevel(need, DateTime.Now), Is.EqualTo(0));

    }

    [Test]
    [TestCase(AnimalNeedEnum.Eating, SpecieEnum.Cat, "Meow")]
    public void EatingStressLevel_RangeCovering_Pass(AnimalNeedEnum need, SpecieEnum specie, string name)
    {
        var animal = this.PetService.CreateAnimal(specie, name);

        int maxHours = animal.CaringSetup.CaringConstraints[need].MaximumIntervalInHours * 2;

        animal = this.PetService.Feed(animal, DateTime.Now);

        for (int hoursInFuture = 1; hoursInFuture <= maxHours; hoursInFuture++)
        {
            int stressLevel = animal.CalculateNeedStressLevel(need, DateTime.Now.AddHours(hoursInFuture));

            Assert.That(stressLevel, Is.GreaterThan(0));
            Assert.That(stressLevel, Is.LessThanOrEqualTo(10));
        }
    }
}