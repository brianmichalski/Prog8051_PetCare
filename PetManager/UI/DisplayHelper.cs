using PetManager.Core.Model.Abstraction;
using PetManager.Core.Model.Type.Tools;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetManager.UI
{
    public class DisplayHelper
    {
        public static void ShowAppHeader()
        {
            var rule = new Rule("[bold blue]Virual Pet Care System[/] - v0.1   [grey](* Press Ctrl+C to Quit)[/]");
            rule.Justification = Justify.Left;
            rule.RuleStyle("red dim");

            AnsiConsole.Clear();
            AnsiConsole.Write(new Rule());
            AnsiConsole.Write(rule);
            AnsiConsole.Write(new Rule());
        }
        public static void DisplayPetInfo(IAnimal animal)
        {
            var table = new Table();

            table.AddColumn("[grey]Animal[/]");
            table.AddColumn(new TableColumn("[grey]Name[/]").Centered());
            table.AddColumn(new TableColumn("[grey]Color[/]").Centered());

            table.AddRow(
                string.Format("[yellow bold]{0}[/]", EnumUtils.GetDescription(animal.Specie)),
                string.Format("[yellow bold]{0}[/]", animal.Name),
                string.Format("[yellow bold]{0}[/]", animal.Color)
            );

            AnsiConsole.Write(table);
        }
        public static Spectre.Console.Color GetStressColor(double stressLevel)
        {
            if (stressLevel > 7)
            {
                return Spectre.Console.Color.Red;
            }
            if (stressLevel > 4)
            {
                return Spectre.Console.Color.Orange1;
            }
            if (stressLevel > 2)
            {
                return Spectre.Console.Color.Orange1;
            }

            return Spectre.Console.Color.Blue;
        }

        public static void DisplayPetMonitor(IAnimal animal)
        {
            BarChart barChart = new BarChart()
                .Width(40)
                .WithMaxValue(10)
                .Label("[green bold underline]Pet Needs Stress Monitor[/]");

            foreach (var need in Program.Service.ListCaringNeeds())
            {
                double stressLevel = Program.Service.GetNeedStressLevel(animal, need.Key, Program.CurrentTime);
                stressLevel = Math.Round(stressLevel);
                barChart.AddItem(need.Value, stressLevel, GetStressColor(stressLevel));
            }
            AnsiConsole.Write(barChart);
            Console.WriteLine();
            Console.WriteLine("* Press [Enter] to continue...");
            Console.ReadLine();

            PromptHelper.PromptForAction();
        }

        public static void DisplayCaringException(string message)
        {
            var panel = new Panel(string.Format("[red bold]{0}[/]", message));

            ShowAppHeader();

            AnsiConsole.Write(panel);

            Console.WriteLine();
            Console.WriteLine("* Press [Enter] to continue...");
            Console.ReadLine();

            PromptHelper.PromptForAction();
        }
    }
}
