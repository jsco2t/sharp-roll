using System;
using SharpRoll.Controllers;

namespace SharpRoll
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //var result = InternalMain(args);
            //System.Environment.Exit(result);
            var inputManager = new InputManager();

            while (true)
            {
                var key = Console.ReadKey(true);
                inputManager.HandleKeyPress(key);
            }
        }

    //     internal static int InternalMain(string[] args)
    //     {
    //         var app = new Microsoft.Extensions.CommandLineUtils.CommandLineApplication();

    //         var rollApp = app.Command("roll", config =>
    //         {
    //             config.OnExecute(() =>
    //             {
    //                 config.ShowHelp();
    //                 return 1;
    //             });
                
    //             config.HelpOption("-? | -h | --help");
    //         });
            
    //         rollApp.Command("d100", config => {
    //             ProcessArgument(config, 100);
    //         });
            
    //         rollApp.Command("d20", config => {
    //             ProcessArgument(config, 20);
    //         });
            
    //         rollApp.Command("d12", config => {
    //             ProcessArgument(config, 12);
    //         });
            
    //         rollApp.Command("d10", config => {
    //             ProcessArgument(config, 10);
    //         });
            
    //         rollApp.Command("d8", config => {
    //             ProcessArgument(config, 8);
    //         });
            
    //         rollApp.Command("d6", config => {
    //             ProcessArgument(config, 6);
    //         });
            
    //         rollApp.Command("d4", config => {
    //             ProcessArgument(config, 4);
    //         });
            
    //         app.HelpOption("-? | -h | --help");

    //         app.OnExecute(() => {
    //             app.ShowHelp();
                
    //             return 1;
    //         });
            
    //         var historyApp = app.Command("history", config =>
    //         {
    //             config.OnExecute(() =>
    //             {
    //                 Dice.GetHistory();
    //                 return 0;
    //             });
                
    //             config.HelpOption("-? | -h | --help");
    //         });
            
    //         var clearHistoryApp = app.Command("clear", config =>
    //         {
    //             config.OnExecute(() =>
    //             {
    //                 Dice.ClearHistory();
    //                 return 0;
    //             });
                
    //             config.HelpOption("-? | -h | --help");
    //         });

    //         var result = app.Execute(args);
    //         return result;
    //     }
        
    //     internal static int ProcessArgument(CommandLineApplication config, int diceSide)
    //     {
    //         var countArg = config.Argument("count", $"how many d{diceSide}'s to roll, ex: 1", false);
    //         var modifierArg = config.Argument("modifier", "what modifier to add, ex: 0", false);
            
    //         var modifierArgOption = config.Option("-m|--modifier <value>",
    //             "When passing a negative modifier pass the input with this option. Ex: -m -1",
    //             CommandOptionType.SingleValue);
            
    //         config.OnExecute(() =>
    //         {
    //             var count = 0;
    //             var modifier = 0;
                
    //             if (!string.IsNullOrWhiteSpace(countArg.Value))
    //             {
    //                 count = int.Parse(countArg.Value);
    //             }
                
    //             if (count == 0)
    //             {
    //                 count = 1;
    //             }
                
    //             if (modifierArgOption.HasValue())
    //             {
    //                 modifier = int.Parse(modifierArgOption.Value());
    //             }
    //             else if (!string.IsNullOrWhiteSpace(modifierArg.Value))
    //             {
    //                 modifier = int.Parse(modifierArg.Value);
    //             }
                    
    //             var diceRoll = Dice.Roll(diceSide, count, modifier);
    //             Console.WriteLine($"Rolled {count} times for Dice: d{diceSide}, with modifier: {modifier}, resulted in: {diceRoll}");

    //             return 0;
    //         });

    //         config.HelpOption("-? | -h | --help");
            
    //         return 0;
    //     }
    }
}