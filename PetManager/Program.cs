using PetManager.Core.Model.Abstraction;
using PetManager.Core.Model.Type;
using PetManager.Service;
using PetManager.UI;
using Spectre.Console;

namespace PetManager;

public class Program
{
    static PromptHelper Prompt { get; } = new PromptHelper();
    static DisplayHelper Display { get; } = new DisplayHelper();

    public static PetService Service = PetService.GetInstance();

    public static IAnimal Animal;

    public static DateTime CurrentTime = DateTime.Now;

    public static void OpenMainMenu(out SpecieEnum specie, out string name, out ColorEnum color)
    {
        specie = PromptHelper.PromptForAnimal();
        name = PromptHelper.PromptForName(specie);
        color = PromptHelper.PromptForColor();
    }

    public static void StartPetCreation()
    {
        DisplayHelper.ShowAppHeader();

        SpecieEnum specie;
        string name;
        ColorEnum color;

        OpenMainMenu(out specie, out name, out color);

        Animal = Service.CreateAnimal(specie, name, color);
        CurrentTime = DateTime.Now;

        DisplayHelper.DisplayPetInfo(Animal);

        if (!AnsiConsole.Confirm("Do you want to proceed?"))
        {
            StartPetCreation();
        }

        PromptHelper.PromptForAction();
    }

    static void Main(string[] args)
    {
        StartPetCreation();
    }
}
