using PetManager.Core.Model.Abstraction;
using PetManager.Core.Model.Type;
using PetManager.Core.Model.Implementation;

namespace PetManager.Core.Factory
{
    public abstract class AnimalFactory
    {
        public static IAnimal CreateAnimal(SpecieEnum specie, string name, ColorEnum color)
        {
            switch (specie)
            {
                case SpecieEnum.Cat:
                    return new Cat(name, color);
                case SpecieEnum.Dog:
                    return new Dog(name, color);
                case SpecieEnum.GuineaPig:
                    return new GuineaPig(name, color);
                case SpecieEnum.Turtle:
                    return new Turtle(name, color);
            }
            throw new NotImplementedException("Specie not recognized.");
        }
    }
}
