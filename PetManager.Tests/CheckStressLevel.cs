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
    [TestCase(SpecieEnum.Dog, "Snoopy")]
    public void EatingStressLevel_BeforeFirstFeeding_Pass(SpecieEnum specie, string name)
    {
        var animal = this.PetService.CreateAnimal(specie, name);

        Assert.That(animal.CalculateEatingStressLevel(DateTime.Now), Is.EqualTo(10));
    }

    [Test]
    [TestCase(SpecieEnum.Cat, "Meow")]
    public void EatingStressLevel_AfterFirstFeeding_Pass(SpecieEnum specie, string name)
    {
        var animal = this.PetService.CreateAnimal(specie, name);
        animal = this.PetService.Feed(animal);

        Assert.That(animal.CalculateEatingStressLevel(DateTime.Now), Is.EqualTo(0));

    }

    [Test]
    [TestCase(SpecieEnum.Cat, "Meow")]
    public void EatingStressLevel_RangeCovering_Pass(SpecieEnum specie, string name)
    {
        var animal = this.PetService.CreateAnimal(specie, name);

        int maxHours = animal.CaringConstraint.EatingIntervalInHours * 2;

        animal = this.PetService.Feed(animal);

        for (int hoursInFuture = 1; hoursInFuture <= maxHours; hoursInFuture++)
        {
            int stressLevel = animal.CalculateEatingStressLevel(DateTime.Now.AddHours(hoursInFuture));

            Assert.That(stressLevel, Is.GreaterThan(0));
            Assert.That(stressLevel, Is.LessThanOrEqualTo(10));
        }
    }
}