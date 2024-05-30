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

    public void Feed(IAnimal animal)
    {
        animal.Eat();
    }

    public void PutToSleep(IAnimal animal)
    {
        animal.Rest();
    }

    public void GiveWater(IAnimal animal)
    {
        animal.Hydrate();
    }

    public void PlayWith(IAnimal animal)
    {
        animal.Play();
    }
}
