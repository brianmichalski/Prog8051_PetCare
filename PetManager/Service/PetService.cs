using PetManager.Core.Factory;
using PetManager.Core.Model;
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
    public Dictionary<AnimalNeedEnum, string> ListCaringNeeds()
    {
        return EnumUtils.MapToDescription<AnimalNeedEnum>();
    }

    public IAnimal CreateAnimal(SpecieEnum specie, string name, ColorEnum color)
    {
        return AnimalFactory.CreateAnimal(specie, name, color);
    }

    public IAnimal Feed(IAnimal animal, DateTime currentTime)
    {
        animal.Eat(currentTime);
        return animal;
    }

    public IAnimal PutToSleep(IAnimal animal, DateTime currentTime)
    {
        animal.Rest(currentTime);
        return animal;
    }

    public IAnimal GiveWater(IAnimal animal, DateTime currentTime)
    {
        animal.Hydrate(currentTime);
        return animal;
    }

    public IAnimal PlayWith(IAnimal animal, DateTime currentTime)
    {
        animal.Play(currentTime);
        return animal;
    }

    public double GetNeedStressLevel(IAnimal animal, AnimalNeedEnum need, DateTime checkingTime)
    {
        return CaringStressCalculator.CalculateNeedStressLevel(animal, need, checkingTime);
    }
}
