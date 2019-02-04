using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using SharpRoll.Model;

namespace SharpRoll.Controllers
{
    public class ClearHistoryHandler: IInputHandler
    {
        private readonly static List<string> keywords = new List<string>() { "clear", "reset", "empty" };
        public List<string> HandlesKeywords
        { 
            get
            {
                return keywords;
            }
        }
        public string HandleInput(string[] inputTokens)
        {
            RollHistory.ClearResults();
            return string.Empty;
        }
    }
}