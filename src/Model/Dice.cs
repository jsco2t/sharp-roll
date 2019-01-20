﻿using System;
using System.Diagnostics;
using SharpRoll.Model;

namespace SharpRoll.Model
{
    public static class Dice
    {
        private static Random rand = new Random((int)System.DateTime.Now.ToFileTime());
        
        public static void ClearHistory()
        {
            RollHistory.ClearResults();
        }
        
        public static void GetHistory()
        {
            var rollHistory = RollHistory.GetResults();

            foreach (var roll in rollHistory)
            {
                Debug.WriteLine($"Rolled {roll.RollCount} times for Dice: d{roll.DiceSideCount}, with modifier: {roll.RollModifier}, resulted in: {roll.Result}, at: {roll.RollTimeStamp}");
            }
        }
        
        public static RollResult Roll(int diceSideCount, int count, int modifier)
        {
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
                result += value;
                
                Debug.WriteLine($"Rolling: d{diceSideCount}, result: {value}");
            }

            if (modifier != 0)
            {
                Debug.WriteLine($"For result: {result}, adjusting with modifier: {modifier}");
                result += modifier;    
            }

            RollHistory.AddResult(diceSideCount, count, modifier, result);
            
            return new RollResult(diceSideCount, count, modifier, result);
        }
    }
}