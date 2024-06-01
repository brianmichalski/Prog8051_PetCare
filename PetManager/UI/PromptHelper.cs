using PetManager.Core.Model.Type.Tools;
using PetManager.Core.Model.Type;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetManager.Core.Model.Abstraction;
using PetManager.Core.Model.Exception;

namespace PetManager.UI
{
    public class PromptHelper
    {
        public static SpecieEnum PromptForAnimal()
        {
            string[] pets = Program.Service.ListSpecies().Values.ToArray();

            var specie = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Choose your [green]pet[/]:")
                    .PageSize(10)
                    .AddChoices(pets));

            return (SpecieEnum)Enum.Parse(typeof(SpecieEnum), specie.Replace(" ", ""));
        }
        public static string PromptForName(SpecieEnum specie)
        {
            DisplayHelper.ShowAppHeader();
            return AnsiConsole.Prompt(
                new TextPrompt<string>(string.Format("What is your [green]{0}'s name[/]?",
                    EnumUtils.GetDescription(specie)))).RemoveMarkup();
        }
        public static ColorEnum PromptForColor()
        {
            DisplayHelper.ShowAppHeader();
            string[] colors = Program.Service.ListColors().Values.ToArray();

            var color = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Choose your pet's [green]color[/]:")
                    .PageSize(10)
                    .AddChoices(colors));

            return (ColorEnum)Enum.Parse(typeof(ColorEnum), color);
        }

        public static void PromptForAction()
        {
            DisplayHelper.ShowAppHeader();

            Dictionary<string, char> actionMenu = new Dictionary<string, char>();
            actionMenu.Add("Open Pet Monitor", 'M');
            actionMenu.Add("Feed Pet", 'F');
            actionMenu.Add("Give Water", 'W');
            actionMenu.Add("Play with", 'P');
            actionMenu.Add("Put to Sleep", 'S');
            actionMenu.Add("Emulate Time Passing", 'T');
            actionMenu.Add("Display Pet Info", 'I');
            actionMenu.Add("Recreate Pet", 'R');

            string actionName = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Choose your [green]ACTION[/]:")
                    .PageSize(10)
                    .AddChoices(actionMenu.Keys.ToArray()));

            char actionOption = actionMenu[actionName];

            try
            {
                switch (actionOption)
                {
                    case 'M':
                        DisplayHelper.DisplayPetMonitor(Program.Animal);
                        break;
                    case 'F':
                        Program.Service.Feed(Program.Animal, Program.CurrentTime);
                        DisplayHelper.DisplayPetMonitor(Program.Animal);
                        break;
                    case 'W':
                        Program.Service.GiveWater(Program.Animal, Program.CurrentTime);
                        DisplayHelper.DisplayPetMonitor(Program.Animal);
                        break;
                    case 'P':
                        Program.Service.PlayWith(Program.Animal, Program.CurrentTime);
                        DisplayHelper.DisplayPetMonitor(Program.Animal);
                        break;
                    case 'S':
                        Program.Service.PutToSleep(Program.Animal, Program.CurrentTime);
                        DisplayHelper.DisplayPetMonitor(Program.Animal);
                        break;
                    case 'T':
                        PromptForTimePassing();
                        break;
                    case 'I':
                        DisplayHelper.DisplayPetInfo(Program.Animal);
                        Console.WriteLine();
                        Console.WriteLine("* Press [Enter] to continue...");
                        Console.ReadLine();
                        PromptForAction();
                        break;
                    case 'R':
                        Program.StartPetCreation();
                        break;
                }
            }
            catch (Exception ex)
            {
                {
                    if (ex is MaximumStressLevelException || ex is MinimumStressLevelException)
                    {
                        DisplayHelper.DisplayCaringException(ex.Message);
                    }
                    else
                        throw;
                }
            }
        }
        static void PromptForTimePassing()
        {
            int hours = AnsiConsole.Prompt(
                new TextPrompt<int>("How many [green]hours[/] have passed?")
                    .PromptStyle("green")
                    .ValidationErrorMessage("[red]That's not a valid time[/]")
                    .Validate(age =>
                    {
                        return age switch
                        {
                            <= 0 => ValidationResult.Error("[red]You must provide at least 1 hour[/]"),
                            >= 24 => ValidationResult.Error("[red]You can not provide a time over 24 hours[/]"),
                            _ => ValidationResult.Success(),
                        };
                    }));

            Program.CurrentTime = Program.CurrentTime.AddHours(hours);

            Console.WriteLine();
            Console.WriteLine(string.Format("You have added {0} hour(s). * Press [Enter] to continue...", hours));
            Console.ReadLine();
            PromptForAction();
        }
    }
}
