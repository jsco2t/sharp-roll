using System;
using System.Text;

namespace SharpRoll.Views
{
    public interface IHelpView
    {
        void WriteWelcomeMessage();

        void WriteHelpMessage();
    }
}