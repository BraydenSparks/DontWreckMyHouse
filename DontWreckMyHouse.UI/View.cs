using DontWreckMyHouse.Core.Models;
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

        public void Display(string message)
        {
            AnsiConsole.Write($"{message}\n");
        }

        public void DiplayErrorMessages(List<string> messages)
        {
            foreach (string message in messages)
            {
               DisplayError(message);
            }
        }

    public void DisplaySuccess(string message)
        {
            AnsiConsole.Write(new Markup($"[green]{message}[/]\n"));
        }
        public void DisplayError(string message)
        {
            AnsiConsole.Write(new Markup($"[red]{message}[/]\n"));
        }

        public void DisplayReport(List<Reservation> reservations )
        {

        }

        public void DisplayError(List<string> errors)
        {
            foreach(string error in errors)
            {
                DisplayError(error);
            }
        }

        public void DisplayHeader(string header)
        {
            AnsiConsole.Write(new Rule($"[bold white]{header}[/]").Alignment(Justify.Left).RuleStyle("green"));
            AnsiConsole.Write("\n\n");
        }
        public void DisplayExit(string message)
        {
            AnsiConsole.Write(new Rule($"[bold white]{message}[/]").Alignment(Justify.Left).RuleStyle("red"));
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
            else
            {
                return false;
            }
        }

        public int PromptInt(string message)
        {
            return AnsiConsole.Prompt(
                new TextPrompt<int>($"{message}")
                    .PromptStyle("grey")
                    .ValidationErrorMessage("[red]Invlid: Not an integer![/]\n")
                    .Validate(input =>
                    {
                        return input switch
                        {
                            <= 0 => ValidationResult.Error("[red]Invalid: Must be greater than 0[/]\n"),
                            _ => ValidationResult.Success(),
                        };
                    }
                )
            );
        }

        public decimal PromptDecimal(string message)
        {
            return AnsiConsole.Prompt(
                new TextPrompt<decimal>($"{message}")
                    .PromptStyle("grey")
                    .ValidationErrorMessage("[red]Invlid: Not a decimal![/]\n")
                    .Validate(input =>
                    {
                        return input switch
                        {
                            <= 0 => ValidationResult.Error("[red]Invalid: Must be greater than 0[/]\n"),
                            _ => ValidationResult.Success(),
                        };
                    }
                )
            );
        }

        public DateTime PromptDateTime(string message, DateTime currentDate)
        {
            return AnsiConsole.Prompt(
                new TextPrompt<DateTime>($"{message}")
                    .PromptStyle("grey")
                    .ValidationErrorMessage("[red]Invlid: Not a DateTime![/]\n")
                    .Validate(input => input > currentDate ? ValidationResult.Success() : ValidationResult.Error("[red]Invalid: Date must be in the future![/]\n"))
                );
        }

        public SearchOption SelectSearchOption(string message)
        {
            SearchOption[] options = Enum.GetValues<SearchOption>();
            Func<SearchOption, string> selector = opt => $"[grey]{opt.ToLabel()}[/]";
            SelectionPrompt<SearchOption> selectionPrompt = new SelectionPrompt<SearchOption>();
            selectionPrompt.Converter = selector;
            selectionPrompt.Title = $"[bold]{message}[/]";
            selectionPrompt.HighlightStyle(new Style(Color.White, Color.Grey23, null, null));
            selectionPrompt.AddChoices(options);
            var option = AnsiConsole.Prompt(selectionPrompt);
            return option;
        }

        public string PromptString(string message)
        {
            return AnsiConsole.Prompt(
                new TextPrompt<string>($"{message}")
                    .PromptStyle("grey")
                    .ValidationErrorMessage("[red]Invlid: Not a string![/]\n")
                    .Validate(input =>
                    {
                        return input switch
                        {
                            "" => ValidationResult.Error("[red]Invalid: String must contain something[/]\n"),
                            _ => ValidationResult.Success(),
                        };
                    }
                )
            );
        }

        public string PromptEmail(string message)
        {
            while (true)
            {
                var input = PromptString($"{message}");
                var peices = input.Split("@");
                if (peices.Length == 2)
                {
                    var domain = peices[1].Split(".");
                    if(domain.Length == 2 && peices[0].Length > 0)
                    {
                        if(domain[0].Length > 0 && domain[1].Length > 1)
                        {
                            return input;
                        }
                    }
                }
                AnsiConsole.Write( new Markup("[red]Error: Incorrect format![/]\n\n"));
            }
        }

        public T PromptSelect<T>(List<T> options, string message)
        {
            return AnsiConsole.Prompt(
                new SelectionPrompt<T>()
                    .Title($"[bold]{message}[/]")
                    .PageSize(20)
                    .MoreChoicesText("[grey](Move up or down to reveal additional options[/])")
                    .HighlightStyle(new Style(Color.White, Color.Grey23, null, null))
                    .AddChoices(options)
                );
        }
    }
}
