using System;
using System.Collections.Generic;
using SharpRoll.Model;

namespace SharpRoll.Controllers
{
    public class RollInputHandler: IInputHandler
    {
        private readonly static List<string> keywords = new List<string>() { "roll" };
        public List<string> HandlesKeywords
        {
            get
            {
                return keywords;
            }
        }
        
        public string HandleInput(string[] inputTokens)
        {
            RollResult rollResult = null;
            var result = "Invalid roll request. Please use this format: roll dNN {optional-count} {optional-modifier}";
            
            if (ValidateInputTokens(inputTokens))
            {
                int? diceSides = 0;
                var diceCount = 0;
                var diceModifier = 0;

                if (inputTokens[1].StartsWith('d'))
                {
                    diceSides = GetDice(inputTokens[1]);
                    diceCount = inputTokens.Length >= 3 ? GetIntValue(inputTokens[2], 1) : 1;
                    diceModifier = inputTokens.Length >= 4 ? GetIntValue(inputTokens[3], 0) : 0;
                }
                else
                {
                    diceCount = inputTokens.Length >= 2 ? GetIntValue(inputTokens[1], 1) : 1;
                    diceSides = inputTokens.Length >= 3 ? GetDice(inputTokens[2]) : null;
                    diceModifier = inputTokens.Length >= 4 ? GetIntValue(inputTokens[3], 0) : 0;
                }
                
                if (null != diceSides)
                {
                    var entropy = GetEntropy(inputTokens);
                    rollResult = Dice.Roll(diceSides.Value, diceCount, diceModifier, entropy);
                    result = GetResultReport(rollResult);
                }
            }

            return result;
        }

        private bool ValidateInputTokens(string[] inputTokens)
        {
            if (inputTokens != null && inputTokens.Length >= 2)
            {
                var ignored = 0;
                if (inputTokens[1].StartsWith('d') || Int32.TryParse(inputTokens[1], out ignored))
                {
                    return true;
                }
            }
            return false;
        }

        private string GetResultReport(RollResult result)
        {
            return $"You rolled a d{result.DiceSideCount} ({result.RollCount} times) with a modifier of {result.RollModifier}, for a total of: {result.Result}";
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

        private int? GetEntropy(string[] inputTokens)
        {
            if (inputTokens != null && inputTokens.Length >= 3)
            {
                var lastIndex = inputTokens.Length - 1;
                if (inputTokens[lastIndex].StartsWith(Strings.EntropyKeyName))
                {
                    var endLength = inputTokens[lastIndex].Length - Strings.EntropyKeyName.Length -1;
                    var startIndex = Strings.EntropyKeyName.Length + 1;
                    var entropyStr = string.Empty;

                    if (startIndex < inputTokens[lastIndex].Length && (startIndex + endLength) <= inputTokens[lastIndex].Length)
                    {
                        entropyStr = inputTokens[lastIndex].Substring(Strings.EntropyKeyName.Length + 1, endLength);
                    }
                    
                    int entropy = 0;
                    
                    if (!string.IsNullOrWhiteSpace(entropyStr) && Int32.TryParse(entropyStr, out entropy))
                    {
                        return entropy;
                    }
                }
            }
            return null;
        }

    }
    
}