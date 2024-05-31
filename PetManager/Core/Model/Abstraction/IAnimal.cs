using PetManager.Core.Model.Type;

namespace PetManager.Core.Model.Abstraction
{
    public interface IAnimal
    {
        SpecieEnum Specie { get; }

        string Name { get; set; }

        public CaringSetup CaringSetup { get; set; }
        public IDictionary<AnimalNeedEnum, DateTime> LastTimeCaringMap { get; }

        public void Rest(DateTime currentTime);

        public void Hydrate(DateTime currentTime);

        public void Eat(DateTime currentTime);

        public void Play(DateTime currentTime);
    }
}
