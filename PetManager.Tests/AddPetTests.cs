using PetManager.Service;

namespace PetManager.Tests;

public class AddPetTests
{
    PetService petService;

    [SetUp]
    public void Setup()
    {
        this.petService = PetService.GetInstance();
    }

    [Test]
    public void ListPetTypesPass()
    {
        Assert.Pass();
    }
}