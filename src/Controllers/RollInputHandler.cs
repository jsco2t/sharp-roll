using System;
using SharpRoll.Model;

namespace SharpRoll.Controllers
{
    public class RollInputHandler: IInputHandler
    {
        public string HandlesKeyword
        {
            get
            {
                return "roll";
            }
        }
        
        public string HandleInput(string[] inputTokens)
        {
            RollResult rollResult = null;
            var result = "Invalid roll request. Please use this format: roll dNN {optional-count} {optional-modifier}";
            
            if (inputTokens.Length >= 2)
            {
                var diceSides = GetDice(inputTokens[1]);
                var diceCount = inputTokens.Length >= 3 ? GetIntValue(inputTokens[2], 1) : 1;
                var diceModifier = inputTokens.Length >= 4 ? GetIntValue(inputTokens[3], 0) : 0;
                if (null != diceSides)
                {
                    rollResult = Dice.Roll(diceSides.Value, diceCount, diceModifier);
                    result = GetResultReport(rollResult);
                }
            }

            return result;
        }

        private string ValidateInputTokens(string[] inputTokens)
        {
            return string.Empty;
        }

        private string GetResultReport(RollResult result)
        {
            return $"You rolled a d{result.DiceSideCount} ({result.RollCount} times) with a modifier of {result.RollModifier} for a total of: {result.Result}";
        }

        private int? GetDice(string dice)
        {
            if (dice.StartsWith(@"d") && dice.Length > 1)
            {
                var diceTemp = dice.Substring(1, dice.Length -1);

                int diceSides = 0;
                
                if (int.TryParse(diceTemp, out diceSides))
                {
                    if (diceSides % 2 == 0)
                    {
                        return diceSides;
                    }
                }
            }

            return null;
        }

        private int GetIntValue(string token, int defaultResult)
        {
            var output = 0;

            if (int.TryParse(token, out output))
            {
                return output;
            }

            return defaultResult;
        }

    }
    
}