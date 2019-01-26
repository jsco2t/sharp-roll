using System;
using System.Text;

namespace SharpRoll.Views
{
    public class ConsoleView : IConsoleView
    {
        public void RemoveCharacter()
        {
            // can we determine if the console has input in it?
            Console.Write("\b");
            Console.Write(" ");
            Console.Write("\b");
        }

        public void ClearLine()
        {
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLineCursor);
        }

        public void Clear()
        {
            Console.Clear();
        }

        public void Write(string output)
        {
            Console.Write(output);
        }

        public void Write(char output)
        {
            Console.Write(output);
        }

        public void WriteLine(string line)
        {
            Console.WriteLine(line);
        }

        public void ReplaceLine(string replacement)
        {
            ClearLine();
            Console.Write(replacement);
        }
    }
}