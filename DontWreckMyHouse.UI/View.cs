using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DontWreckMyHouse.UI
{
    public class View
    {
        private readonly ConsoleIO io;

        public View(ConsoleIO io)
        {
            this.io = io;
        }

        public MainMenuOption SelectMainMenuOption()
        {
            MainMenuOption[] options = Enum.GetValues<MainMenuOption>();
            Func<MainMenuOption, string> selector = opt => $"[grey]{opt.ToLabel()}[/]";
            SelectionPrompt<MainMenuOption> selectionPrompt = new SelectionPrompt<MainMenuOption>();
            selectionPrompt.Converter = selector;
            selectionPrompt.Title = "[bold]Select a Menu Option:[/]";
            selectionPrompt.HighlightStyle(new Style(Color.White, Color.Grey23, null, null));
            selectionPrompt.AddChoices(options);
            var option = AnsiConsole.Prompt(selectionPrompt);
            return option;
        }

        public void DisplayExit(string message)
        {
            io.PrintLine(message + "\n");
        }

        public void DisplayHeader(string header)
        {
            io.PrintLine(header + "\n");
        }

        public bool PromptYesNo(string message)
        {
            SelectionPrompt<string> selection = new SelectionPrompt<string>();
            selection.Title = $"[yellow bold]{message}[/]";
            selection.HighlightStyle(new Style(Color.White, Color.Grey23, null, null));
            selection.AddChoices(new[] { "[grey]Yes[/]", "[grey]No[/]" });
            var option = AnsiConsole.Prompt(selection);
            if (option == "[grey]Yes[/]")
            {
                return true;
            }
            else return false;
        }
    }
}
