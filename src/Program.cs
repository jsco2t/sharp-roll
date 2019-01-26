using System;
using SharpRoll.Controllers;
using SharpRoll.Views;

namespace SharpRoll
{
    public class Program
    {
        internal static IConsoleView consoleView = new ConsoleView();
        internal static IHelpView helpView = new HelpView();

        public static void Main(string[] args)
        {
            (new HelpView()).WriteWelcomeMessage();
            var inputManager = new InputManager(consoleView, helpView);

            while (true)
            {
                var key = Console.ReadKey(true);
                inputManager.HandleKeyPress(key);
            }
        }
    }
}