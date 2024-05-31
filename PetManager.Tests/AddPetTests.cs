using PetManager.Core.Model.Implementation;
using PetManager.Core.Model.Type;
using PetManager.Service;

namespace PetManager.Tests;

public class AddPetTests
{
    PetService PetService;

    [SetUp]
    public void Setup()
    {
        this.PetService = PetService.GetInstance();
    }

    [Test]
    [TestCase(SpecieEnum.Cat, "Meow")]
    public void CreateAnimalCheckNotNull_Pass(SpecieEnum specie, string name)
    {
        var animal = this.PetService.CreateAnimal(specie, name);
        Assert.That(animal, Is.Not.Null);
    }

    [Test]
    [TestCase(SpecieEnum.Cat, "Meow", ExpectedResult = typeof(Cat))]
    [TestCase(SpecieEnum.Dog, "Snoopy", ExpectedResult = typeof(Dog))]
    [TestCase(SpecieEnum.GuineaPig, "Porky", ExpectedResult = typeof(GuineaPig))]
    [TestCase(SpecieEnum.Turtle, "Speed", ExpectedResult = typeof(Turtle))]
    public Type CreateAnimalCheckTypes_Pass(SpecieEnum specie, string name)
    {
        var animal = this.PetService.CreateAnimal(specie, name);

        return animal.GetType();
    }

    [Test]
    [TestCase(SpecieEnum.Cat, "Meow", ExpectedResult = "Meow")]
    [TestCase(SpecieEnum.Dog, "Snoopy", ExpectedResult = "Snoopy")]
    [TestCase(SpecieEnum.GuineaPig, "Porky", ExpectedResult = "Porky")]
    [TestCase(SpecieEnum.Turtle, "Speed", ExpectedResult = "Speed")]
    public string CreateAnimalCheckName(SpecieEnum specie, string name)
    {
        var animal = this.PetService.CreateAnimal(specie, name);

        return animal.Name;
    }
}