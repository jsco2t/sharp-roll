using System;
using System.Text;

namespace SharpRoll.Views
{
    public class HelpView : IHelpView
    {
        internal static IConsoleView consoleView = null;
        private IConsoleView ConsoleView
        {
            get
            {
                if (null == consoleView)
                {
                    consoleView = new ConsoleView();
                }

                return consoleView;
            }
        }

        public void WriteWelcomeMessage()
        {
            var message = new StringBuilder();
            message.Append($"################################################################################## {Environment.NewLine}");
            message.Append($"Welcome to SharpRoll - Please enjoy rolling those crit-failures! {Environment.NewLine}");
            message.Append($"{Environment.NewLine}");
            message.Append($"Supported commands: {Environment.NewLine}");
            message.Append(GetCommands());
            message.Append($"################################################################################## {Environment.NewLine}");

            ConsoleView.WriteLine(message.ToString());
        }

        public void WriteHelpMessage()
        {
            var message = new StringBuilder();
            message.Append($"{Environment.NewLine}################################################################################## {Environment.NewLine}");
            message.Append($"I'm sorry but I can't do that... {Environment.NewLine}");
            message.Append($"{Environment.NewLine}");
            message.Append($"I can do the following commands: {Environment.NewLine}");
            message.Append(GetCommands());
            message.Append($"################################################################################## {Environment.NewLine}");

            ConsoleView.WriteLine(message.ToString());
        }

        private string GetCommands()
        {
            var message = new StringBuilder();
            message.Append($"  roll dNN COUNT MODIFIER: {Environment.NewLine}");
            message.Append($"  roll COUNT dNN MODIFIER: {Environment.NewLine}");
            message.Append($"    Roll a dice based on side count (ex: d20) {Environment.NewLine}");
            message.Append($"    Values 'COUNT' and 'MODIFIER' are optional {Environment.NewLine}");
            message.Append($"    Example: roll d20 5 10 (rolls a d20 5 times and adds a modifier of 10) {Environment.NewLine}");
            message.Append($"    Example: roll d20 (rolls a d20 1 times with no modifier) {Environment.NewLine}");
            message.Append($"{Environment.NewLine}");
            message.Append($"  exit: {Environment.NewLine}");
            message.Append($"    Exits the application {Environment.NewLine}");
            message.Append($"{Environment.NewLine}");
            message.Append($"  history: {Environment.NewLine}");
            message.Append($"    Returns the current roll history {Environment.NewLine}");
            return message.ToString();
        }
    }
}