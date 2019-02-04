using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using SharpRoll.Model;

namespace SharpRoll.Controllers
{
    public class RollHistoryHandler: IInputHandler
    {
        private readonly static List<string> keywords = new List<string>() { "history", "logs", "log" };
        public List<string> HandlesKeywords
        { 
            get
            {
                return keywords;
            }
        }
        public string HandleInput(string[] inputTokens)
        {
            return FormatedHistoryResults();
        }

        private string FormatedHistoryResults()
        {
            StringBuilder sb = new StringBuilder();

            var historyResults = RollHistory.GetResults();

            if (null != historyResults && historyResults.Any())
            {
                sb.Append($"{Environment.NewLine}############################################ {Environment.NewLine}");
                foreach (var historyItem in historyResults)
                {
                    sb.Append($" {historyItem.DiceCount} d{historyItem.RollResults.First().DiceSideCount} dice (with a modifier of: {historyItem.Modifier}, and entropy of: {historyItem.RollResults.First().Entropy}) rolled for a result of: {historyItem.RollTotal} {Environment.NewLine}");
                }
                sb.Append($"############################################ {Environment.NewLine}");
            }

            return sb.ToString();
        }
    }
}