using PetManager.Core.Model.Abstraction;
using PetManager.Core.Model.Type;
using PetManager.Core.Model.Implementation;

namespace PetManager.Core.Factory
{
    public abstract class AnimalFactory
    {
        public static IAnimal CreateAnimal(SpecieEnum specie, string name)
        {
            switch (specie)
            {
                case SpecieEnum.Cat:
                    return new Cat(name);
                case SpecieEnum.Dog:
                    return new Dog(name);
                case SpecieEnum.GuineaPig:
                    return new GuineaPig(name);
                case SpecieEnum.Turtle:
                    return new Turtle(name);
            }
            throw new NotImplementedException("Specie not recognized.");
        }
    }
}
