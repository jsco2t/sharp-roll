using System;
using System.Text;
using System.Collections.Generic;

namespace SharpRoll.Controllers
{
    public class InputManager
    {
        private StringBuilder inputBuffer = new StringBuilder();
        private List<string> priorInputBuffer = new List<string>();
        private int? priorInputBufferIndex = null;

        private Dictionary<string, IInputHandler> inputHandlers;

        public InputManager()
        {
            inputHandlers = new Dictionary<string, IInputHandler>();

            var rollInputHandler = new RollInputHandler();

            inputHandlers.Add(rollInputHandler.HandlesKeyword, rollInputHandler);
        }

        public string HandleInput(string input)
        {
            var inputTokens = GetInputTokens(input);

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
            
            switch (key.Key)
            {
                case ConsoleKey.UpArrow:
                    var priorInput = GetPriorInputBufferItem();
                    Console.WriteLine(priorInput);
                    inputBuffer.Clear();
                    inputBuffer.Append(priorInput);
                    break;

                case ConsoleKey.Enter:
                case ConsoleKey.End:
                    
                    var result = HandleInput(inputBuffer.ToString());
                    inputBuffer.Clear();
                    priorInputBufferIndex = 0;
                    //Console.WriteLine();
                    //Console.WriteLine(result);
                    Console.Write($"{Environment.NewLine}{result}{Environment.NewLine}");
                    break;

                case ConsoleKey.Backspace:
                case ConsoleKey.Delete:
                    Console.Write("\b");
                    Console.Write(" ");
                    Console.Write("\b");
                    TrimInputBuffer();
                    break;

                default:
                    Console.Write(key.KeyChar);
                    inputBuffer.Append(key.KeyChar);
                    break;
            }

            //return string.Empty;
        }

        private string[] GetInputTokens(string input)
        {
            return input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        }

        private void AddToPriorInputBuffer(string input)
        {
            priorInputBuffer.Insert(0, input);

            if (priorInputBuffer.Count > 25)
            {
                priorInputBuffer.RemoveAt(priorInputBuffer.Count -1);
            }
        }

        private string GetPriorInputBufferItem()
        {
            var result = string.Empty;
            int index = priorInputBufferIndex.HasValue ? priorInputBufferIndex.Value : 0;

            if (index <= priorInputBuffer.Count - 1)
            {
                result = priorInputBuffer[index];
                index++;
                priorInputBufferIndex = index;
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

    }
    
}