using System;
using System.Collections.Generic;

namespace SharpRoll.Controllers
{
    public class ExitInputHandler: IInputHandler
    {
        private readonly static List<string> keywords = new List<string>(){ "exit", "stop", "quit" };
        public List<string> HandlesKeywords 
        { 
            get
            {
                return keywords;
            }
        }
        
        public string HandleInput(string[] inputTokens)
        {
            Environment.Exit(0);
            return string.Empty;
        }
    }
}