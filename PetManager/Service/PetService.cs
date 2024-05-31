using PetManager.Core.Factory;
using PetManager.Core.Model.Abstraction;
using PetManager.Core.Model.Type;
using PetManager.Core.Model.Type.Tools;

namespace PetManager.Service;

public class PetService
{
    private static PetService Instance = new PetService();

    private PetService() { }

    public static PetService GetInstance()
    {
        return Instance;
    }

    public Dictionary<SpecieEnum, string> ListSpecies()
    {   
        return EnumUtils.MapToDescription<SpecieEnum>();
    }
    public Dictionary<ColorEnum, string> ListColors()
    {
        return EnumUtils.MapToDescription<ColorEnum>();
    }

    public IAnimal CreateAnimal(SpecieEnum specie, string name)
    {
        return AnimalFactory.CreateAnimal(specie, name);
    }

    public IAnimal Feed(IAnimal animal)
    {
        animal.Eat();
        return animal;
    }

    public IAnimal PutToSleep(IAnimal animal)
    {
        animal.Rest();
        return animal;
    }

    public IAnimal GiveWater(IAnimal animal)
    {
        animal.Hydrate();
        return animal;
    }

    public IAnimal PlayWith(IAnimal animal)
    {
        animal.Play();
        return animal;
    }
}
