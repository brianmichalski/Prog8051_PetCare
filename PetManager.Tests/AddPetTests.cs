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
    [TestCase(SpecieEnum.Cat, "Meow", ColorEnum.Black)]
    public void CreateAnimalCheckNotNull_Pass(SpecieEnum specie, string name, ColorEnum color)
    {
        var animal = this.PetService.CreateAnimal(specie, name, color);
        Assert.That(animal, Is.Not.Null);
    }

    [Test]
    [TestCase(SpecieEnum.Cat, "Meow", ColorEnum.Black, ExpectedResult = typeof(Cat))]
    [TestCase(SpecieEnum.Dog, "Snoopy", ColorEnum.Yellow, ExpectedResult = typeof(Dog))]
    [TestCase(SpecieEnum.GuineaPig, "Porky", ColorEnum.Gray, ExpectedResult = typeof(GuineaPig))]
    [TestCase(SpecieEnum.Turtle, "Speed", ColorEnum.White, ExpectedResult = typeof(Turtle))]
    public Type CreateAnimalCheckTypes_Pass(SpecieEnum specie, string name, ColorEnum color)
    {
        var animal = this.PetService.CreateAnimal(specie, name, color);

        return animal.GetType();
    }

    [Test]
    [TestCase(SpecieEnum.Cat, "Meow", ColorEnum.Yellow, ExpectedResult = "Meow")]
    [TestCase(SpecieEnum.Dog, "Snoopy", ColorEnum.Yellow, ExpectedResult = "Snoopy")]
    [TestCase(SpecieEnum.GuineaPig, "Porky", ColorEnum.Yellow, ExpectedResult = "Porky")]
    [TestCase(SpecieEnum.Turtle, "Speed", ColorEnum.Yellow, ExpectedResult = "Speed")]
    public string CreateAnimalCheckName(SpecieEnum specie, string name, ColorEnum color)
    {
        var animal = this.PetService.CreateAnimal(specie, name, color);

        return animal.Name;
    }
}