using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using SharpRoll.Views;
using SharpRoll.Model;

namespace SharpRoll.Controllers
{
    public class InputManager
    {
        private StringBuilder inputBuffer = new StringBuilder();
        private List<string> priorInputBuffer = new List<string>();
        private int? priorInputBufferIndex = null;
        private Dictionary<string, IInputHandler> inputHandlers;
        private IConsoleView consoleView;
        private IHelpView helpView;
        private Stopwatch stopWatch = new Stopwatch();
        private int entropy = 0;

        public InputManager(IConsoleView consoleView, IHelpView helpView)
        {
            inputHandlers = new Dictionary<string, IInputHandler>();
            this.consoleView = consoleView;
            this.helpView = helpView;

            var rollInputHandler = new RollInputHandler();
            var exitInputHandler = new ExitInputHandler();
            var rollHistoryHandler = new RollHistoryHandler();
            var clearHistoryHandler = new ClearHistoryHandler();

            rollInputHandler.HandlesKeywords.ForEach(x => inputHandlers.Add(x, rollInputHandler));
            exitInputHandler.HandlesKeywords.ForEach(x => inputHandlers.Add(x, exitInputHandler));
            rollHistoryHandler.HandlesKeywords.ForEach(x => inputHandlers.Add(x, rollHistoryHandler));
            clearHistoryHandler.HandlesKeywords.ForEach(x => inputHandlers.Add(x, clearHistoryHandler));
        }

        public string HandleInput(string input)
        {
            var inputTokens = GetInputTokens($"{input} {Strings.EntropyKeyName}:{entropy}");

            var keyword = inputTokens.Length > 0 ? inputTokens[0] : string.Empty;

            if (inputHandlers.ContainsKey(keyword))
            {
                AddToPriorInputBuffer(input);
                return inputHandlers[keyword].HandleInput(inputTokens);
            }

            return string.Empty;
        }

        public void HandleKeyPress(ConsoleKeyInfo key)
        {
            var priorInput = string.Empty;

            switch (key.Key)
            {
                case ConsoleKey.LeftArrow:
                case ConsoleKey.RightArrow:
                    break;
                
                case ConsoleKey.UpArrow:
                    priorInput = GetPriorInputBufferItem();
                    consoleView.ReplaceLine(priorInput);
                    inputBuffer.Clear();
                    inputBuffer.Append(priorInput);
                    break;

                case ConsoleKey.DownArrow:
                    priorInput = GetPriorInputBufferItem(searchForward: false);
                    consoleView.ReplaceLine(priorInput);
                    inputBuffer.Clear();
                    inputBuffer.Append(priorInput);
                    break;

                case ConsoleKey.Enter:
                case ConsoleKey.End:
                    var input = inputBuffer.ToString();
                    var result = HandleInput(input);
                    inputBuffer.Clear();
                    priorInputBufferIndex = 0;
                    
                    if (String.IsNullOrWhiteSpace(result))
                    {
                        if (!CommandKeyWordIsSupported(input))
                        {
                            helpView.WriteHelpMessage();
                        }
                        else
                        {
                            consoleView.WriteLine("");
                        }
                    }
                    else
                    {
                        consoleView.Write($"{Environment.NewLine}{result}{Environment.NewLine}");
                    }

                    stopWatch.Reset();
                    break;

                case ConsoleKey.Backspace:
                case ConsoleKey.Delete:
                    consoleView.RemoveCharacter();
                    TrimInputBuffer();
                    break;

                default:
                    consoleView.Write(key.KeyChar);
                    inputBuffer.Append(key.KeyChar);
                    UpdateEntropy();
                    break;
            }

            //return string.Empty;
        }

        private string[] GetInputTokens(string input)
        {
            return input.ToLowerInvariant().Split(' ', StringSplitOptions.RemoveEmptyEntries);
        }

        private bool CommandKeyWordIsSupported(string input)
        {
            if (!string.IsNullOrWhiteSpace(input))
            {
                var inputTokens = GetInputTokens(input);

                if (inputTokens != null && inputTokens.Length > 0)
                {
                    return inputHandlers.ContainsKey(inputTokens[0]);
                }
            }

            return false;
        }

        private void AddToPriorInputBuffer(string input)
        {
            priorInputBuffer.Insert(0, input);

            if (priorInputBuffer.Count > 25)
            {
                priorInputBuffer.RemoveAt(priorInputBuffer.Count -1);
            }
        }

        private string GetPriorInputBufferItem(bool searchForward = true)
        {
            var result = string.Empty;
            int index = priorInputBufferIndex.HasValue ? priorInputBufferIndex.Value : 0;

            if (index <= priorInputBuffer.Count - 1)
            {
                if (!searchForward && index >= 0)
                {
                    index = index > 0 ? index - 1 : priorInputBuffer.Count - 1; // if we have searched until the index is 0 - then reset
                    result = priorInputBuffer[index];
                    priorInputBufferIndex = index;
                }
                else
                {
                    result = priorInputBuffer[index];
                    index++;
                    priorInputBufferIndex = index >= priorInputBuffer.Count ? 0 : index; // if index is past the end then we need to start over
                }
                
            }

            return result;
        }

        private void TrimInputBuffer()
        {
            if (inputBuffer.Length > 0)
            {
                inputBuffer.Remove(inputBuffer.Length - 1, 1);
            }
        }

        private void UpdateEntropy()
        {
            if (!stopWatch.IsRunning)
            {
                stopWatch.Start();
            }
            else
            {
                var currentTicks = stopWatch.ElapsedTicks;
                stopWatch.Restart();
                entropy += (int)currentTicks;
            }
        }
    }
}