using System;
using System.Collections.Generic;
using System.Diagnostics;
using SharpRoll.Model;

namespace SharpRoll.Model
{
    public static class Dice
    {
        private static int lastSeed = (int) System.DateTime.Now.ToFileTime();
        private static Random rand = new Random(lastSeed);
        
        public static void ClearHistory()
        {
            RollHistory.ClearResults();
        }
        
        public static RollSummary Roll(int diceSideCount, int count, int modifier, int? updatedSeed = null)
        {
            var rolls = new List<RollResult>();

            if (updatedSeed.HasValue && updatedSeed.Value != 0)
            {
                var seedToUse = updatedSeed.Value;

                if (seedToUse != lastSeed)
                {
                    lastSeed = seedToUse;
                    rand = new Random(seedToUse);
                }
            }

            var result = 0;

            if (count == 0)
            {
                count = 1;
            }
            
            if (diceSideCount == 0 || diceSideCount < 1)
            {
                throw new ArgumentException("dice side is an invalid value it myst be greater then 1");
            }

            for (var i = 0; i < count; i++)
            {
                var value = rand.Next(1, diceSideCount);
                rolls.Add(new RollResult(diceSideCount, lastSeed, value));
            }

            if (modifier != 0)
            {
                Debug.WriteLine($"For result: {result}, adjusting with modifier: {modifier}");
                result += modifier;    
            }

            var rollSummary = new RollSummary(rolls, modifier);
            RollHistory.AddResult(rollSummary);
            
            return rollSummary;
        }
    }
}