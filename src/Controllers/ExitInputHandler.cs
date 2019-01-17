using System;
using System.Collections.Generic;

namespace SharpRoll.Controllers
{
    public class ExitInputHandler: IInputHandler
    {
        public string HandlesKeyword 
        { 
            get
            {
                return "exit";
            }
        }
        public string HandleInput(string[] inputTokens)
        {
            Environment.Exit(0);
            return string.Empty;
        }
    }
}