using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using SharpRoll.Model;

namespace SharpRoll.Model
{
    public class RollSummary
    {
        public int RollTotal = 0;

        public List<RollResult> RollResults = new List<RollResult>();

        public int Modifier = 0;

        public int DiceCount
        {
            get
            {
                return RollResults.Count;
            }
        }

        public RollSummary(List<RollResult> rollResults, int modifier)
        {
            if (null == rollResults)
            {
                throw new ArgumentNullException("rollResults");
            }

            if (!rollResults.Any())
            {
                throw new ArgumentException("rollResults must be non empty");
            }

            Modifier = modifier;

            foreach (var roll in rollResults)
            {
                RollTotal += roll.Result + Modifier;
                RollResults.Add(roll);
            }
        }
    }
}