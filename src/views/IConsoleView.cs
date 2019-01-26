using System;

namespace SharpRoll.Views
{
    public interface IConsoleView
    {
        void RemoveCharacter();

        void ClearLine();

        void Clear();

        void Write(string output);

        void Write(char output);

        void WriteLine(string line);

        void ReplaceLine(string replacement);
    }
}